using Automatonymous;
using System;
using MassTransit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Booking
{
    public class RestaurantBooking : SagaStateMachineInstance
    {
        /// <summary>
        /// идентификатор для соотнесения всех сообщений друг с другом
        /// </summary>
        public Guid CorrelationId { get; set; }

        /// <summary>
        /// текущее состояние саги (по умолчанию присутствуют Initial - 1 и Final - 2)
        /// </summary>
        public int CurrentState { get; set; }

        /// <summary>
        ///идентификатор заказа/бронирования
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        ///идентификатор клиента
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        ///маркировка для "композиции" событий (наш случай с кухней и забронированным столом)
        /// </summary>
        public int ReadyEventStatus { get; set; }

        /// <summary>
        /// пометка о том, что наша заявка просрочена
        /// </summary>
        public Guid? ExpirationId { get; set; }
    }
}
