using MessageDrivenArchitecture.Models;
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
                Console.WriteLine("asyncbook - асинхронно забронировать столик" +
                    "\nsyncbook - синхронно забронировать столик" +
                    "\nasyncunbook - асинхронно снять бронь" +
                    "\nsyncunbook - синхронно снять бронь\n");

                var choice = Console.ReadLine().ToLower();
                Console.WriteLine();
                if (!_choices.Contains(choice))
                {
                    Console.WriteLine("Введите asyncbook, syncbook, asyncunbook, syncunbook (регистр не важен)\n");
                    continue;
                }
                //Console.WriteLine("Введите количество мест:");
                //if (!int.TryParse(Console.ReadLine(), out var seatsCount) && (seatsCount < 1 || seatsCount > 12))
                //{
                //    Console.WriteLine("Введите число от 1 до 12:");
                //    continue;
                //}
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
                Console.WriteLine("Спасибо за звонок!");
                var ts = stopWatch.Elapsed;
                Console.WriteLine($"{ts.Seconds:00}:{ts.Milliseconds:00}\n");
            }
        }
    }
}