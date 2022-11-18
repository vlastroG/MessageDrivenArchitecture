using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Restaurant.Kitchen.Consumers;
using Restaurant.Kitchen.Models;

namespace Restaurant.Kitchen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<KitchenTableBookedConsumer>(
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
                            });

                        x.AddConsumer<KitchenBookingRequestFaultConsumer>();

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

                    services.AddSingleton<Manager>();

                    services.AddMassTransitHostedService(true);
                });
    }
}