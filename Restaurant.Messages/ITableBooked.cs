using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Messages
{
    public interface ITableBooked
    {
        /// <summary>
        /// Идентификационный номер заказа
        /// </summary>
        public Guid OrderId { get; }

        /// <summary>
        /// Идентификационный номер клиента
        /// </summary>
        public Guid ClientId { get; }

        /// <summary>
        /// Предзаказанное блюдо
        /// </summary>
        public Dish? PreOrder { get; }

        /// <summary>
        /// Статус брони стола
        /// </summary>
        public bool Success { get; }
    }
}
