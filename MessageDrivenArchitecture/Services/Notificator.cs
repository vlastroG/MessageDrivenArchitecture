using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDrivenArchitecture.Services
{
    public static class Notificator
    {
        public static void SayHello()
        {
            Task.Run(async () =>
            {
                await Task.Delay(1500);
                Console.WriteLine("Здравствуйте! Доступны следующие команды:\n" +
                    "asyncbook - асинхронно забронировать столик" +
                    "\nsyncbook - синхронно забронировать столик" +
                    "\nasyncunbook - асинхронно снять бронь" +
                    "\nsyncunbook - синхронно снять бронь\n");
            });
        }

        public static void CommandError()
        {
            Console.WriteLine("Введите asyncbook, syncbook, asyncunbook, syncunbook (регистр не важен)\n");
        }

        public static void SayGoodbye()
        {
            Task.Run(async () =>
            {
                await Task.Delay(1200);
                Console.WriteLine("Спасибо за звонок!\n");
            });
        }

        public static void SendMessage(string message)
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                Console.WriteLine(message);
            });
        }
    }
}
