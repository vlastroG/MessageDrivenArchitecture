using MassTransit;
using Restaurant.Kitchen.Models;
using Restaurant.Messages;
using Restaurant.Messages.Implements;

namespace Restaurant.Kitchen.Consumers
{
    /// <summary>
    /// Консьюмер заказа брони стола на кухне
    /// </summary>
    public class KitchenTableBookedConsumer : IConsumer<IBookingRequest>
    {
        private readonly Manager _manager;
        private readonly IBus _bus;

        public KitchenTableBookedConsumer(Manager manager, IBus bus)
        {
            _manager = manager;
            _bus = bus;
        }

        /// <summary>
        /// Обработать заказ на кухне
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<IBookingRequest> context)
        {
            Console.WriteLine($"[OrderId: {context.Message.OrderId} CreationDate: {context.Message.CreationDate}]");
            Console.WriteLine("Trying time: " + DateTime.Now);

            await Task.Delay(5000);

            if (_manager.CheckKitchenReady(context.Message.OrderId, context.Message.PreOrder))
                await _bus.Publish<IKitchenReady>(new KitchenReady(context.Message.OrderId, true));
        }
    }
}
