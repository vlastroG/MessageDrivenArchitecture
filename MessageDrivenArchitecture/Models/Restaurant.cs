using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDrivenArchitecture.Models
{
    public class Restaurant
    {
        /// <summary>
        /// Столы в ресторане
        /// </summary>
        private readonly List<Table> _tables = new();


        /// <summary>
        /// Конструктор ресторана с заданным количеством столов
        /// </summary>
        /// <param name="tablesCount">Количество столов в ресторане в диапазоне [5, 100]</param>
        public Restaurant(int tablesCount)
        {
            if (tablesCount < 5 || tablesCount > 100)
            {
                throw new ArgumentException(
                    $"Количество столов в ресторане должно быть в диапазоне [5, 100], получено: {tablesCount}");
            }
            for (int i = 1; i <= tablesCount; i++)
            {
                _tables.Add(new Table(i));
            }
        }


        /// <summary>
        /// Синхронный заказ столика
        /// </summary>
        /// <param name="countOfPersons">Количество персон от 1 до 12</param>
        /// <exception cref="ArgumentException"></exception>
        public void BookTable(int countOfPersons)
        {
            if (countOfPersons < 1 || countOfPersons > 12)
            {
                throw new ArgumentException(
                    $"Количество персон должно быть не менее 1 и не более 12. Получено: {countOfPersons}");
            }
            Console.WriteLine("Добрый день! Сейчас подберем Вам столик!");

            var table = _tables.FirstOrDefault(t => t.SeatsCount >= countOfPersons && t.State == State.Free);
            Console.WriteLine(table is null
                ? "К сожалению мы не можем Вам предложить подходящий столик...Обратитесь позднее!"
                : $"Готово! Ваш столик под номером {table.Id}");
        }

        public void BookTableAsync(int countOfPersons)
        {
            if (countOfPersons < 1 || countOfPersons > 12)
            {
                throw new ArgumentException(
                    $"Количество персон должно быть не менее 1 и не более 12. Получено: {countOfPersons}");
            }
            Console.WriteLine("Добрый день! Сейчас подберем Вам столик!");
            Task.Run(async () =>
            {
                var table = _tables.FirstOrDefault(t => t.SeatsCount >= countOfPersons && t.State == State.Free);

                await Task.Delay(3000);
                table?.SetState(State.Booked);
                Console.WriteLine(table is null
                    ? "К сожалению мы не можем Вам предложить подходящий столик...Обратитесь позднее!"
                    : $"Готово! Ваш столик под номером {table.Id}");
            });
        }
    }
}
