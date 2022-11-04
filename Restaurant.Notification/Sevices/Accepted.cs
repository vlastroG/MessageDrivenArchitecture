namespace Restaurant.Notification.Sevices
{
    /// <summary>
    /// Статус принятия заказа
    /// </summary>
    [Flags]
    public enum Accepted
    {
        /// <summary>
        /// Отклонен
        /// </summary>
        Rejected = 0,

        /// <summary>
        /// Заказ на кухню
        /// </summary>
        Kitchen = 1,

        /// <summary>
        /// Заказ на бронь стола
        /// </summary>
        Booking = 2,

        /// <summary>
        /// Заказ на кухню или бронь стола
        /// </summary>
        All = Kitchen | Booking
    }
}
