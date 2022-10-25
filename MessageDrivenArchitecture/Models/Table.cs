using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDrivenArchitecture.Models
{
    public class Table
    {
        public State State { get; private set; }

        public int SeatsCount { get; }

        public int Id { get; }

        /// <summary>
        /// Конструктор стола по id
        /// </summary>
        /// <param name="id">Идентификационный номер</param>
        public Table(int id) : this(id, Random.Shared.Next(1, 13)) { }

        /// <summary>
        /// Конструктор стола по id и количеству мест
        /// </summary>
        /// <param name="id">Идентификационный номер</param>
        /// <param name="seatsCount">Количество мест должно быть от 1 до 12 включая концы</param>
        /// <exception cref="ArgumentException">Исключение, если количество мест некорректно</exception>
        public Table(int id, int seatsCount)
        {
            Id = id;
            if (seatsCount < 1 || seatsCount > 12)
                throw new ArgumentException($"У стола может быть от 1 до 12 мест, получено: {seatsCount}");
            SeatsCount = seatsCount;
        }

        /// <summary>
        /// Устанавливает столу новое состояние
        /// </summary>
        /// <param name="state"></param>
        /// <returns>True, если состояние изменено, иначе false</returns>
        public bool SetState(State state)
        {
            if (state == State) return false;
            State = state;
            return true;
        }
    }
}
