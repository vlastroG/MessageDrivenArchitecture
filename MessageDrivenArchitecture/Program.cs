using MessageDrivenArchitecture.Models;
using System.Diagnostics;

namespace MessageDrivenArchitecture
{
    internal class Program
    {
        private static readonly string[] _choices = new string[] { "a", "s" };

        static void Main(string[] args)
        {
            Restaurant restaurant = new(50);
            while (true)
            {
                Console.WriteLine("Здравствуйте! На какое количество персон хабронировать столик для Вас?" +
                    "\nA - асинхронно" +
                    "\nS - синхронно");

                var choice = Console.ReadLine().ToLower();
                if (_choices.Contains(choice))
                {
                    Console.WriteLine("Введите A или S (регистр не важен)");
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
                if (choice == "a")
                {
                    stopWatch.Start();
                    restaurant.BookTableAsync(seatsCount);
                    stopWatch.Stop();
                }
                else
                {
                    stopWatch.Start();
                    restaurant.BookTable(seatsCount);
                    stopWatch.Stop();
                }
                Console.WriteLine("Спасибо за звонок!");
                var ts = stopWatch.Elapsed;
                Console.WriteLine($"{ts.Seconds:00}:{ts.Milliseconds:00}");
            }
        }
    }
}