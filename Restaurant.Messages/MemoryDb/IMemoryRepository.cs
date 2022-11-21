using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Messages.MemoryDb
{
    /// <summary>
    /// Репозиторий в памяти приложения
    /// </summary>
    /// <typeparam name="T">Тип, хранимый в репозитории</typeparam>
    public interface IMemoryRepository<T> where T : class
    {
        /// <summary>
        /// Добавляет или обновляет уже имеющуюся сущность
        /// </summary>
        /// <param name="entity"></param>
        public void AddOrUpdate(T entity);

        /// <summary>
        /// Получить перечисление всех объектов в репозитории
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Get();
    }
}
