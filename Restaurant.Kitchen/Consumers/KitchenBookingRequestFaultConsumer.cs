using MassTransit;
using Restaurant.Messages;

namespace Restaurant.Kitchen.Consumers
{
    /// <summary>
    /// Консьюмер отмены заказа на кухне
    /// </summary>
    public class KitchenBookingRequestFaultConsumer : IConsumer<Fault<IBookingRequest>>
    {
        /// <summary>
        /// Обработать отмену заказа на кухне
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Consume(ConsumeContext<Fault<IBookingRequest>> context)
        {
            return Task.CompletedTask;
        }
    }
}
