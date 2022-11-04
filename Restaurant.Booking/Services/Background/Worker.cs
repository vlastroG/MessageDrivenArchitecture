﻿using MassTransit;
using Messaging;
using Microsoft.Extensions.Hosting;
using Restaurant.Messages.Implements;
using System.Text;

namespace Restaurant.Booking.Services.Background
{
    public class Worker : BackgroundService
    {
        private readonly IBus _bus;

        private readonly Restaurant.Booking.Models.Restaurant _restaurant;

        public Worker(IBus bus, Restaurant.Booking.Models.Restaurant restaurant)
        {
            _bus = bus;
            _restaurant = restaurant;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(10000, stoppingToken);
                Console.WriteLine("Привет! Желаете забронировать столик?");
                var result = await _restaurant.BookTableAsync(1);
                await _bus.Publish(new TableBooked(NewId.NextGuid(), NewId.NextGuid(), result ?? false),
                    context => context.Durable = false, stoppingToken);
            }
        }
    }
}
