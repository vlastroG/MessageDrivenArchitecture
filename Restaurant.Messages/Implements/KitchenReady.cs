using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Messages.Implements
{
    public class KitchenReady : IKitchenReady
    {
        /// <summary>
        /// Конструктор сообщения готовности кухни выполнить заказ
        /// </summary>
        /// <param name="orderId">Идентификатор заказа</param>
        /// <param name="ready">Статус готовности кухни выполнить заказ</param>
        public KitchenReady(Guid orderId, bool ready)
        {
            OrderId = orderId;
            Ready = ready;
        }

        public Guid OrderId { get; }

        public bool Ready { get; }
    }
}
