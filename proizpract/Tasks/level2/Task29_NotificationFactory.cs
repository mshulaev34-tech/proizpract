using proizpract.Common;
using proizpract.Enums;
using proizpract.Models;
using proizpract.Models.proizpract.Models;
using System;

namespace proizpract.Tasks.Level2
{
    [Common.TaskInfoAttribute("Level-2", 29)]
    internal class Task29_NotificationFactory : ITask
    {
        public string Title => "Паттерн «Фабрика» (уведомления)";

        public string Description => "Реализация фабрики уведомлений через паттерн «Фабричный метод».";

        public string[] Requirements => new string[]
        {
            "Интерфейс INotification с методом Send(string message)",
            "Реализации: EmailNotification, SmsNotification, PushNotification",
            "NotificationFactory.Create(NotificationType type)",
            "enum NotificationType { Email, Sms, Push }",
            "Продемонстрировать создание и отправку через фабрику"
        };

        public void Run()
        {
            string testMessage = "Ваш заказ №12345 подтверждён!";

            Console.WriteLine("=== Отправка через фабрику ===");
            foreach (NotificationType type in Enum.GetValues(typeof(NotificationType)))
            {
                INotification notification = NotificationFactory.Create(type);
                Console.Write($"{notification.ChannelName,-6}: ");
                notification.Send(testMessage);
            }

            Console.Write("\nВыберите тип уведомления (Email/Sms/Push): ");
            string typeInput = Console.ReadLine();
            if (Enum.TryParse(typeInput, true, out NotificationType selectedType))
            {
                Console.Write("Введите текст сообщения: ");
                string userMessage = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(userMessage))
                {
                    INotification notification = NotificationFactory.Create(selectedType);
                    notification.Send(userMessage);
                }
                else
                {
                    Console.WriteLine("Пустое сообщение.");
                }
            }
            else
            {
                Console.WriteLine("Неизвестный тип уведомления.");
            }
        }
    }
}