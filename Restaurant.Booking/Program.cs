using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Restaurant.Booking.Consumers;
using Restaurant.Booking.Services.Background;

namespace Restaurant.Booking
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<RestaurantBookingRequestConsumer>(
                            configurator =>
                            {
                                configurator.UseScheduledRedelivery(r =>
                                {
                                    r.Intervals(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20),
                                        TimeSpan.FromSeconds(30));
                                });
                                configurator.UseMessageRetry(
                                    r =>
                                    {
                                        r.Incremental(3, TimeSpan.FromSeconds(1),
                                            TimeSpan.FromSeconds(2));
                                    }
                                );
                            })
                            .Endpoint(e =>
                            {
                                e.Temporary = true;
                            });

                        x.AddConsumer<BookingRequestFaultConsumer>()
                            .Endpoint(e =>
                            {
                                e.Temporary = true;
                            });

                        x.AddSagaStateMachine<RestaurantBookingSaga, RestaurantBooking>()
                            .Endpoint(e => e.Temporary = true)
                            .InMemoryRepository();

                        x.AddDelayedMessageScheduler();

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("kangaroo.rmq.cloudamqp.com", 5672, "ueiosuvi", h =>
                            {
                                h.Username("ueiosuvi");
                                h.Password("KdYyQ2jvIP7hVpOP1IZLEyQkrRPI8MW8");
                            });

                            cfg.UseDelayedMessageScheduler();
                            cfg.UseInMemoryOutbox();
                            cfg.ConfigureEndpoints(context);
                        });
                    });
                    services.AddMassTransitHostedService(true);

                    services.AddTransient<RestaurantBooking>();
                    services.AddTransient<RestaurantBookingSaga>();
                    services.AddTransient<Models.Restaurant>();

                    services.AddHostedService<Worker>();
                });
    }
}