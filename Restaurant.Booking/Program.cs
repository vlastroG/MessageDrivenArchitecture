using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using MassTransit;
using MassTransit.Audit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;
using Restaurant.Booking.Consumers;
using Restaurant.Booking.Services.Background;
using Restaurant.Messages;
using Restaurant.Messages.MemoryDb;

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
                        services.AddSingleton<IMessageAuditStore, AuditStore>();

                        var serviceProvider = services.BuildServiceProvider();
                        var auditStore = serviceProvider.GetService<IMessageAuditStore>();

                        x.AddConsumer<RestaurantBookingRequestConsumer>();

                        x.AddSagaStateMachine<RestaurantBookingSaga, RestaurantBooking>()
                            .InMemoryRepository();

                        x.AddDelayedMessageScheduler();

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("kangaroo.rmq.cloudamqp.com", 5672, "ueiosuvi", h =>
                            {
                                h.Username("ueiosuvi");
                                h.Password("KdYyQ2jvIP7hVpOP1IZLEyQkrRPI8MW8");
                            });
                            cfg.UsePrometheusMetrics(serviceName: "booking_service");
                            cfg.Durable = false;
                            cfg.UseDelayedMessageScheduler();
                            cfg.UseInMemoryOutbox();
                            cfg.ConfigureEndpoints(context);

                            cfg.ConnectSendAuditObservers(auditStore);
                            cfg.ConnectConsumeAuditObserver(auditStore);
                        });
                    });

                    services.Configure<MassTransitHostOptions>(options =>
                    {
                        options.WaitUntilStarted = true;
                        options.StartTimeout = TimeSpan.FromSeconds(30);
                        options.StopTimeout = TimeSpan.FromMinutes(1);
                    });

                    services.AddTransient<RestaurantBooking>();
                    services.AddTransient<RestaurantBookingSaga>();
                    services.AddTransient<Models.Restaurant>();
                    services.AddSingleton<IMemoryRepository<IBookingRequest>, MemoryRepository<IBookingRequest>>();

                    services.AddHostedService<Worker>();
                });
    }
}