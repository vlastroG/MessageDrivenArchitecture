using System.Collections.Concurrent;

namespace Restaurant.Notification.Sevices
{
    /// <summary>
    /// Уведомитель для заказа
    /// </summary>
    public class Notifier
    {
        /// <summary>
        /// Послать сообщение клиенту по его заказу
        /// </summary>
        /// <param name="orderId">Id заказа</param>
        /// <param name="clientId">Id клиента</param>
        /// <param name="message">Сообщение клиенту</param>
        public void Notify(Guid orderId, Guid clientId, string message)
        {
            Console.WriteLine($"[OrderID: {orderId}] Уважаемый клиент {clientId}! {message}");
        }
    }
}
