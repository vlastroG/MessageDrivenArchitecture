using MassTransit.Audit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Restaurant.Booking
{
    /// <summary>
    /// Хранитель сообщений (логов)
    /// </summary>
    public class AuditStore : IMessageAuditStore
    {
        private readonly ILogger _logger;

        public AuditStore(ILogger<AuditStore> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Сохранить сообщение в лог
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public Task StoreMessage<T>(T message, MessageAuditMetadata metadata) where T : class
        {
            _logger.Log(LogLevel.Information,
                JsonSerializer.Serialize(metadata) + "\n" + JsonSerializer.Serialize(message));
            return Task.CompletedTask;
        }
    }
}
