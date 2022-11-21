using Restaurant.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Booking
{
    /// <summary>
    /// Модель заказа
    /// </summary>
    public class BookingRequestModel
    {
        private readonly List<string> _messageIds = new List<string>();

        /// <summary>
        /// Id заказа
        /// </summary>
        public Guid OrderId { get; private set; }

        /// <summary>
        /// Id клиента
        /// </summary>
        public Guid ClientId { get; private set; }

        /// <summary>
        /// Предзаказанное блюдо
        /// </summary>
        public Dish? PreOrder { get; private set; }

        /// <summary>
        /// Время создания заказа
        /// </summary>
        public DateTime CreationDate { get; private set; }

        /// <summary>
        /// Конструктор модели заказа
        /// </summary>
        /// <param name="orderId">Id заказа</param>
        /// <param name="clientId">Id клиента</param>
        /// <param name="preOrder">Предзаказанное блюдо</param>
        /// <param name="creationDate">Дата создания заказа</param>
        /// <param name="messageId">Сообщение</param>
        public BookingRequestModel(Guid orderId, Guid clientId, Dish? preOrder, DateTime creationDate, string messageId)
        {
            _messageIds.Add(messageId);

            OrderId = orderId;
            ClientId = clientId;
            PreOrder = preOrder;
            CreationDate = creationDate;
        }

        /// <summary>
        /// Обновить модель заказа по данным из новой модели
        /// </summary>
        /// <param name="model">Новая модель заказа</param>
        /// <param name="messageId">сообщение</param>
        /// <returns>Обновленная модель заказа</returns>
        public BookingRequestModel Update(BookingRequestModel model, string messageId)
        {
            _messageIds.Add(messageId);

            OrderId = model.OrderId;
            ClientId = model.ClientId;
            PreOrder = model.PreOrder;
            CreationDate = model.CreationDate;

            return this;
        }

        /// <summary>
        /// Проверить, было ли уже получено данное сообщение
        /// </summary>
        /// <param name="messageId">сообщение для проверки</param>
        /// <returns></returns>
        public bool CheckMessageId(string messageId)
        {
            return _messageIds.Contains(messageId);
        }
    }
}
