using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Messages
{
    public interface IKitchenReady
    {
        /// <summary>
        /// Уникальный идентификатор заказа
        /// </summary>
        public Guid OrderId { get; }

        /// <summary>
        /// Статус готовности кухни выполнить заказ
        /// </summary>
        public bool Ready { get; }
    }
}
