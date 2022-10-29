using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Booking.Models
{
    /// <summary>
    /// Состояние объекта
    /// </summary>
    public enum State
    {
        /// <summary>
        /// Свободен
        /// </summary>
        Free = 0,

        /// <summary>
        /// Забронирован
        /// </summary>
        Booked = 1
    }
}
