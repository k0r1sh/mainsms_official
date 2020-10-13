using System;
using System.Collections.Generic;
using System.Text;

namespace MainSms
{
    /// <summary>
    /// Данные создаваемого получателя
    /// </summary>
    public struct SendingInfo
    {
        /// <summary>
        /// Группы получателей через запятую
        /// </summary>
        public string include;
        /// <summary>
        /// Исключенные группы получателей через запятую
        /// </summary>
        public string exclude;
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string message;
        /// <summary>
        /// Имя отправителя
        /// </summary>
        public string sender;
        /// <summary>
        /// Время отправки сообщения в часовом поясе кабинета 03.10.2031 17:00
        /// </summary>
        public string run_at;
        /// <summary>
        /// Интервал для плавной рассылки, например отправлять каждые 10 минут
        /// </summary>
        public string slowtime;
        /// <summary>
        /// Количество сообщений для плавной рассылки, от 10 до 10000
        /// </summary>
        public string slowsize;
        /// <summary>
        /// Название рассылки
        /// </summary>
        public string name;

        public Dictionary<string, string> toDictionary()
        {
            Dictionary<string, string> resultDctionary = new Dictionary<string, string>()
            {
                { "include", include },
                { "exclude", exclude },
                { "message", message },
                { "sender", sender },
                { "run_at", run_at },
                { "slowtime", slowtime },
                { "slowsize", slowsize },
                { "name", name }
            };
            return resultDctionary;
        }
    }
}
