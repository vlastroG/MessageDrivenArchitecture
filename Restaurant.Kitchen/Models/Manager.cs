using MassTransit;
using Restaurant.Messages.Implements;
using Restaurant.Messages;

namespace Restaurant.Kitchen.Models
{
    /// <summary>
    /// Менеджер кухни
    /// </summary>
    public class Manager
    {
        private readonly IBus _bus;

        public Manager(IBus bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// Проверить, готова ли кухня выполнить заказ
        /// </summary>
        /// <param name="orderId">Id заказа</param>
        /// <param name="dish">Блюдо для заказа</param>
        /// <returns></returns>
        public bool CheckKitchenReady(Guid orderId, Dish? dish)
        {
            return true;
        }
    }
}
