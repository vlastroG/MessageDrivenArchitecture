using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Messages
{
    /// <summary>
    /// Модель заказа брони стола
    /// </summary>
    public interface IBookingRequest
    {
        /// <summary>
        /// Id заказа
        /// </summary>
        public Guid OrderId { get; }

        /// <summary>
        /// Id клиента
        /// </summary>
        public Guid ClientId { get; }

        /// <summary>
        /// Предзаказанное блюдо
        /// </summary>
        public Dish? PreOrder { get; }

        /// <summary>
        /// Время создания заказа
        /// </summary>
        public DateTime CreationDate { get; }
    }
}
