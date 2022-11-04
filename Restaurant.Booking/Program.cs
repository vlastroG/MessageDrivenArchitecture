using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("kangaroo.rmq.cloudamqp.com", 5672, "ueiosuvi", h =>
                            {
                                h.Username("ueiosuvi");
                                h.Password("KdYyQ2jvIP7hVpOP1IZLEyQkrRPI8MW8");
                            });

                            cfg.ConfigureEndpoints(context);
                        });
                    });
                    services.AddMassTransitHostedService(true);

                    services.AddTransient<Restaurant.Booking.Models.Restaurant>();

                    services.AddHostedService<Worker>();
                });
    }
}