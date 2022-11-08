using MassTransit;
using Messaging;
using Microsoft.Extensions.Hosting;
using Restaurant.Messages;
using Restaurant.Messages.Implements;
using System.Text;

namespace Restaurant.Booking.Services.Background
{
    public class Worker : BackgroundService
    {
        private readonly IBus _bus;


        public Worker(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(10000, stoppingToken);
                Console.WriteLine("Привет! Желаете забронировать столик?");
                var dateTime = DateTime.Now;
                await _bus.Publish(
                    (IBookingRequest)new BookingRequest(NewId.NextGuid(), NewId.NextGuid(), null, dateTime),
                    stoppingToken);
            }
        }
    }
}
