using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Booking.Consumers
{
    /// <summary>
    /// Отмена заказа
    /// </summary>
    public interface IBookingExpire
    {
        /// <summary>
        /// Id заказа
        /// </summary>
        public Guid OrderId { get; }
    }
}
