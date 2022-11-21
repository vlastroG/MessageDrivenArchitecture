using MassTransit;
using Restaurant.Messages.Implements;
using Restaurant.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Messages.MemoryDb;
using Microsoft.Extensions.Logging;

namespace Restaurant.Booking.Consumers
{
    /// <summary>
    /// Потребитель сообщений запросов бронирования
    /// </summary>
    public class RestaurantBookingRequestConsumer : IConsumer<IBookingRequest>
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Ресторан, в котором происходит бронь
        /// </summary>
        private readonly Restaurant.Booking.Models.Restaurant _restaurant;

        /// <summary>
        /// Хранилище сообщений запросов заказов в памяти приложения
        /// </summary>
        private readonly IMemoryRepository<IBookingRequest> _memoryRepository;

        public RestaurantBookingRequestConsumer(
            Restaurant.Booking.Models.Restaurant restaurant,
            IMemoryRepository<IBookingRequest> memoryRepository,
            ILogger<RestaurantBookingRequestConsumer> logger)
        {
            _restaurant = restaurant;
            _memoryRepository = memoryRepository;
            _logger = logger;
        }

        /// <summary>
        /// Обрабатывает запрос бронирования стола
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<IBookingRequest> context)
        {
            _logger.Log(LogLevel.Information, $"[OrderId: {context.Message.OrderId}]");
            var model = _memoryRepository.Get().FirstOrDefault(i => i.OrderId == context.Message.OrderId);

            if (model is null)
            {
                _logger.Log(LogLevel.Debug, "First time message");
                _memoryRepository.AddOrUpdate(context.Message);
                var result = await _restaurant.BookTableAsync(1);
                await context.Publish<ITableBooked>(new TableBooked(context.Message.OrderId, result ?? false, Dish.Pizza));
                return;
            }
            _logger.Log(LogLevel.Debug, "Second time message");
        }
    }
}
