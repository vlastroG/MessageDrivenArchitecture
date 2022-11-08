﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Messages.Implements
{
    public class BookingRequest : IBookingRequest
    {
        public BookingRequest(Guid orderId, Guid clientId, Dish? preOrder, DateTime creationDate)
        {
            OrderId = orderId;
            ClientId = clientId;
            PreOrder = preOrder;
            CreationDate = creationDate;
        }

        public Guid OrderId { get; }
        public Guid ClientId { get; }
        public Dish? PreOrder { get; }

        public DateTime CreationDate { get; }
    }
}
