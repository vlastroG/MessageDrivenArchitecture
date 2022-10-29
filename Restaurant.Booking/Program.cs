using System.Diagnostics;

namespace Restaurant.Booking
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var rest = new Restaurant.Booking.Models.Restaurant();
            while (true)
            {
                await Task.Delay(10000);

                Console.WriteLine("Привет! Желаете забронировать столик?");

                var stopWatch = new Stopwatch();

                stopWatch.Start();
                rest.BookTableAsync(1);
                stopWatch.Stop();

                Console.WriteLine("Спасибо за Ваше обращение!");
                var ts = stopWatch.Elapsed;
                Console.WriteLine($"{ts.Seconds:00}:{ts.Milliseconds:00}");
            }
        }
    }
}