using MessageDrivenArchitecture.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MessageDrivenArchitecture.Models
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
            System.Timers.Timer timer = new System.Timers.Timer(20000);
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
                Notificator.SendMessage("Время бронирования столов истекло. Все столы свободны!");
            });
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
            Notificator.SendMessage("Добрый день! Сейчас подберем Вам столик!");

            Table table = null;
            lock (_lockTables)
            {
                table = _tables.FirstOrDefault(t => t.SeatsCount >= countOfPersons && t.State == State.Free);
                table?.SetState(State.Booked);
            }
            Notificator.SendMessage(table is null
                ? "К сожалению мы не можем Вам предложить подходящий столик...Обратитесь позднее!"
                : $"Готово! Ваш столик под номером {table.Id}\n");
        }

        public void BookTableAsync(int countOfPersons)
        {
            if (countOfPersons < 1 || countOfPersons > 12)
            {
                throw new ArgumentException(
                    $"Количество персон должно быть не менее 1 и не более 12. Получено: {countOfPersons}");
            }
            Notificator.SendMessage("Добрый день! Сейчас подберем Вам столик!");
            Task.Run(async () =>
            {
                Table table = null;
                lock (_lockTables)
                {
                    table = _tables.FirstOrDefault(t => t.SeatsCount >= countOfPersons && t.State == State.Free);
                    table?.SetState(State.Booked);
                }
                await Task.Delay(3000);
                Notificator.SendMessage(table is null
                    ? "К сожалению мы не можем Вам предложить подходящий столик...Обратитесь позднее!"
                    : $"Готово! Ваш столик под номером {table.Id}\n");
            });
        }

        /// <summary>
        /// Синхронное снятие брони по номеру стола
        /// </summary>
        /// <param name="tableId">Номер стола</param>
        /// <exception cref="ArgumentException">Исключение, если номер стола < 1</exception>
        public void UnBookTable(int tableId)
        {
            if (tableId < 1)
            {
                throw new ArgumentException(
                    $"Номер стола должен быть не менее 1, получено: {tableId}");
            }
            Notificator.SendMessage("Добрый день! Сейчас снимем бронь.");
            Table table = null;
            lock (_lockTables)
            {
                table = _tables.FirstOrDefault(t => t.Id == tableId);
                table?.SetState(State.Free);
            }
            Notificator.SendMessage(table is null
                ? $"У нас нет стола с номером {tableId}"
                : $"Готово! Cтолик под номером {table.Id} теперь точно свободен!\n");
        }

        /// <summary>
        /// Асинхронное снятие брони стола по его номеру
        /// </summary>
        /// <param name="tableId">Номер стола</param>
        /// <exception cref="ArgumentException">Исключение, если номер стола < 1</exception>
        public void UnBookTableAsync(int tableId)
        {
            if (tableId < 1)
            {
                throw new ArgumentException(
                    $"Номер стола должен быть не менее 1, получено: {tableId}");
            }
            Notificator.SendMessage("Добрый день! Сейчас снимем бронь.");
            Task.Run(async () =>
            {
                Table table = null;
                lock (_lockTables)
                {
                    table = _tables.FirstOrDefault(t => t.Id == tableId);
                    table?.SetState(State.Free);
                }
                await Task.Delay(3000);
                Notificator.SendMessage(table is null
                     ? $"У нас нет стола с номером {tableId}"
                     : $"Готово! Cтолик под номером {table.Id} теперь точно свободен!\n");
            });
        }
    }
}
