using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Messages
{
    /// <summary>
    /// Оповещатель
    /// </summary>
    public interface INotify
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
        /// Сообщение клиенту о заказе
        /// </summary>
        public string Message { get; }
    }
}
