using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Messages.Implements
{
    public class BookingCancellation : IBookingCancellation
    {
        /// <summary>
        /// Конструктор отмены заказа
        /// </summary>
        /// <param name="orderId">Id отменяемого заказа</param>
        public BookingCancellation(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; }
    }
}
