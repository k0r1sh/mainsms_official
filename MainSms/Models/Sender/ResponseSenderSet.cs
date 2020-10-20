using System;
using System.Collections.Generic;
using System.Text;

namespace MainSms
{
    public class ResponseSenderSet : Response
    {
        /// <summary>
        /// Ответ на запрос установки имени по умолчанию
        /// </summary>
        public ResponseSenderSet(string data) : base(data) { }

        /// <summary>
        /// Имя отправителя
        /// </summary>
        public string sender
        {
            get { return getValue("sender"); }
        }

        /// <summary>
        /// Результат
        /// </summary>
        public string result
        {
            get { return getValue("result"); }
        }
    }
}