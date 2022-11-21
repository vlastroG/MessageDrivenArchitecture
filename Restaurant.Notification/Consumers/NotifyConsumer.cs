using MassTransit;
using Restaurant.Messages;
using Restaurant.Notification.Sevices;

namespace Restaurant.Notification.Consumers
{
    /// <summary>
    /// Консьюмер уведомлений
    /// </summary>
    public class NotifyConsumer : IConsumer<INotify>
    {
        private readonly Notifier _notifier;

        public NotifyConsumer(Notifier notifier)
        {
            _notifier = notifier;
        }

        /// <summary>
        /// Уведомить клиента о его заказе
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Consume(ConsumeContext<INotify> context)
        {
            _notifier.Notify(context.Message.OrderId, context.Message.ClientId, context.Message.Message);

            return context.ConsumeCompleted;
        }
    }
}
