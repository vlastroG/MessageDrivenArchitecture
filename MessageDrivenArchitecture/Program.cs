using MessageDrivenArchitecture.Models;
using MessageDrivenArchitecture.Services;
using System.Diagnostics;

namespace MessageDrivenArchitecture
{
    internal class Program
    {
        private static readonly string[] _choices = new string[] {
            "asyncbook",
            "syncbook",
            "asyncunbook",
            "syncunbook" };

        static void Main(string[] args)
        {
            Restaurant restaurant = new(50);
            while (true)
            {
                Notificator.SayHello();

                var choice = Console.ReadLine().ToLower();
                Console.WriteLine();
                if (!_choices.Contains(choice))
                {
                    Notificator.CommandError();
                    continue;
                }
                var seatsCount = 1;


                var stopWatch = new Stopwatch();
                switch (choice)
                {
                    case "syncbook":
                        stopWatch.Start();
                        restaurant.BookTable(seatsCount);
                        stopWatch.Stop();
                        break;
                    case "asyncbook":
                        stopWatch.Start();
                        restaurant.BookTableAsync(seatsCount);
                        stopWatch.Stop();
                        break;
                    case "asyncunbook":
                        stopWatch.Start();
                        restaurant.UnBookTableAsync(Random.Shared.Next(1, 51));
                        stopWatch.Stop();
                        break;
                    case "syncunbook":
                        stopWatch.Start();
                        restaurant.UnBookTableAsync(Random.Shared.Next(1, 51));
                        stopWatch.Stop();
                        break;
                    default:
                        continue;
                }
                Notificator.SayGoodbye();
                var ts = stopWatch.Elapsed;
                Console.WriteLine($"{ts.Seconds:00}:{ts.Milliseconds:00}\n");
            }
        }
    }
}