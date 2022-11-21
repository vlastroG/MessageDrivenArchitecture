using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Messages
{
    /// <summary>
    /// Отмена заказа
    /// </summary>
    public interface IBookingCancellation
    {
        /// <summary>
        /// Id заказа для отмены
        /// </summary>
        public Guid OrderId { get; }
    }
}
