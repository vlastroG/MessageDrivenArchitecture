﻿using System.Collections.Concurrent;

namespace Restaurant.Notification.Sevices
{
    public class Notifier
    {
        public void Notify(Guid orderId, Guid clientId, string message)
        {
            Console.WriteLine($"[OrderID: {orderId}] Уважаемый клиент {clientId}! {message}");
        }
    }
}
