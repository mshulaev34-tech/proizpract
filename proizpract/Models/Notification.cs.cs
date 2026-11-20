using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Models
{
    using System;

    namespace proizpract.Models
    {


        

        /// <summary>
        /// Notification sent via email.
        /// </summary>
        public class EmailNotification : Common.INotification
        {
            public string ChannelName => "Email";

            public void Send(string message)
            {
                Console.WriteLine($"[EMAIL] Отправлено письмо: \"{message}\"");
            }
        }

        /// <summary>
        /// Notification sent via SMS.
        /// </summary>
        public class SmsNotification : Common.INotification
        {
            public string ChannelName => "SMS";

            public void Send(string message)
            {
                Console.WriteLine($"[SMS] Отправлено сообщение: \"{message}\"");
            }
        }

        /// <summary>
        /// Notification sent via push notification.
        /// </summary>
        public class PushNotification : Common.INotification
        {
            public string ChannelName => "Push";

            public void Send(string message)
            {
                Console.WriteLine($"[PUSH] Push-уведомление: \"{message}\"");
            }
        }

        /// <summary>
        /// Factory that creates notification instances based on the specified type.
        /// </summary>
        public static class NotificationFactory
        {
            /// <summary>
            /// Creates a notification instance for the specified type.
            /// </summary>
            public static Common.INotification Create(Enums.NotificationType type)
            {
                switch (type)
                {
                    case Enums.NotificationType.Email: return new EmailNotification();
                    case Enums.NotificationType.Sms: return new SmsNotification();
                    case Enums.NotificationType.Push: return new PushNotification();
                    default: throw new ArgumentOutOfRangeException(nameof(type), type, "Неизвестный тип уведомления.");
                }
            }
        }
    }
}
