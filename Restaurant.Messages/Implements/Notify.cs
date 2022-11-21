﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Messages.Implements
{
    public class Notify : INotify
    {
        /// <summary>
        /// Уведомитель по заказу для клиента
        /// </summary>
        /// <param name="orderId">Id заказа</param>
        /// <param name="clientId">Id клиента</param>
        /// <param name="message">Cообщение уведомления</param>
        public Notify(Guid orderId, Guid clientId, string message)
        {
            OrderId = orderId;
            ClientId = clientId;
            Message = message;
        }

        public Guid OrderId { get; }

        public Guid ClientId { get; }

        public string Message { get; }
    }
}
