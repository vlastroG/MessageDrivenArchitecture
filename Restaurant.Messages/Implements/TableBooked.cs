using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Messages.Implements
{
    public class TableBooked : ITableBooked
    {
        /// <summary>
        /// Конструктор сообщения брони стола
        /// </summary>
        /// <param name="orderId">Идентификационный номер заказа</param>
        /// <param name="clientId">Идентификационный номер клиента</param>
        /// <param name="success">Статус успешности брони</param>
        /// <param name="preOrder">Предзаказанное блюдо</param>
        public TableBooked(Guid orderId, Guid clientId, bool success, Dish? preOrder = null)
        {
            OrderId = orderId;
            ClientId = clientId;
            Success = success;
            PreOrder = preOrder;
        }

        public Guid OrderId { get; }

        public Guid ClientId { get; }

        public Dish? PreOrder { get; }

        public bool Success { get; }
    }
}
