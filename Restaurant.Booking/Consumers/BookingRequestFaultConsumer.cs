using MassTransit;
using Restaurant.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Booking.Consumers
{
    /// <summary>
    /// Консьюмер ошибки запроса бронирования
    /// </summary>
    public class BookingRequestFaultConsumer : IConsumer<Fault<IBookingRequest>>
    {
        /// <summary>
        /// Обработка ошибки бронирования
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Consume(ConsumeContext<Fault<IBookingRequest>> context)
        {
            Console.WriteLine($"[OrderId {context.Message.Message.OrderId}] Отмена в зале");
            return Task.CompletedTask;
        }
    }
}
