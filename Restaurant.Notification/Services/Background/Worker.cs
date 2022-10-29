using Messaging;
using System.Text;

namespace Restaurant.Notification.Services.Background
{
    public class Worker : BackgroundService
    {
        private readonly Consumer _consumer;

        public Worker()
        {
            _consumer = new Consumer("BookingNotification", "localhost");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Receive((sender, args) =>
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body); //декодируем
                Console.WriteLine(" [x] Received {0}", message);
            });
        }
    }
}
