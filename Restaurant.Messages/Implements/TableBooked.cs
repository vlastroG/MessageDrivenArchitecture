using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Messages.Implements
{
    /// <summary>
    /// Сообщение брони стола
    /// </summary>
    public class TableBooked : ITableBooked
    {
        /// <summary>
        /// Конструктор сообщения брони стола
        /// </summary>
        /// <param name="orderId">Идентификационный номер заказа</param>
        /// <param name="clientId">Идентификационный номер клиента</param>
        /// <param name="success">Статус успешности брони</param>
        /// <param name="preOrder">Предзаказанное блюдо</param>
        public TableBooked(Guid orderId, bool success, Dish? preOrder = null)
        {
            OrderId = orderId;
            Success = success;
            PreOrder = preOrder;
        }

        /// <summary>
        /// Id заказа
        /// </summary>
        public Guid OrderId { get; }

        /// <summary>
        /// Предзаказанное блюдо
        /// </summary>
        public Dish? PreOrder { get; }

        /// <summary>
        /// Статус брони
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Время создания заказа
        /// </summary>
        public DateTime CreationDate { get; }
    }
}
