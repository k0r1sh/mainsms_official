using System;
using System.Collections.Generic;
using System.Text;

namespace MainSms
{
    public class ResponseSenderCreate : Response
    {
        /// <summary>
        /// Ответ на добавление имени отправителя
        /// </summary>
        public ResponseSenderCreate(string data) : base(data) { }

        /// <summary>
        /// Имя отправителя
        /// </summary>
        public string sender
        {
            get { return getValue("sender"); }
        }
    }
}