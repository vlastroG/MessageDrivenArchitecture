using Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Restaurant.Booking.Models
{
    public class Restaurant
    {
        /// <summary>
        /// Столы в ресторане
        /// </summary>
        private readonly List<Table> _tables = new();

        /// <summary>
        /// Объект для синхронизации списка столов
        /// </summary>
        private readonly object _lockTables = new object();

        /// <summary>
        /// Продюсер уведомлений
        /// </summary>
        private readonly Producer _producer = new("BookingNotification", "localhost");

        /// <summary>
        /// Стандартный конструктор ресторана с 10 столами
        /// </summary>
        public Restaurant() : this(10) { }

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
            UnbookAll();
        }

        private void UnbookAll()
        {
            System.Timers.Timer timer = new System.Timers.Timer(60000);
            timer.AutoReset = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            timer.Start();
        }

        private async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            await Task.Run(() =>
            {
                lock (_lockTables)
                {
                    foreach (var table in _tables)
                    {
                        table.SetState(State.Free);
                    }
                }
                Console.WriteLine("Время бронирования столов истекло. Все столы свободны!");
            });
        }

        public async Task<bool?> BookTableAsync(int countOfPersons)
        {
            if (countOfPersons < 1 || countOfPersons > 12)
            {
                throw new ArgumentException(
                    $"Количество персон должно быть не менее 1 и не более 12. Получено: {countOfPersons}");
            }
            Console.WriteLine("Добрый день! Сейчас подберем Вам столик!");

            Table table = null;
            await Task.Delay(3000);
            lock (_lockTables)
            {
                table = _tables.FirstOrDefault(t => t.SeatsCount >= countOfPersons && t.State == State.Free);
                return table?.SetState(State.Booked);
            }
        }


        /// <summary>
        /// Асинхронное снятие брони стола по его номеру
        /// </summary>
        /// <param name="tableId">Номер стола</param>
        /// <exception cref="ArgumentException">Исключение, если номер стола < 1</exception>
        public async Task<bool?> UnBookTableAsync(int tableId)
        {
            if (tableId < 1)
            {
                throw new ArgumentException(
                    $"Номер стола должен быть не менее 1, получено: {tableId}");
            }
            Console.WriteLine("Добрый день! Сейчас снимем бронь.");
            Table table = null;
            await Task.Delay(3000);
            lock (_lockTables)
            {
                table = _tables.FirstOrDefault(t => t.Id == tableId);
                return table?.SetState(State.Free);
            }
        }
    }
}
