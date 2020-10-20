using System;
using System.Collections.Generic;
using System.Text;

namespace MainSms
{
    public class ResponseSenderRemove : Response
    {
        /// <summary>
        /// Ответ на удаление имени отправителя
        /// </summary>
        public ResponseSenderRemove(string data) : base(data) { }

        /// <summary>
        /// Имя отправителя
        /// </summary>
        public string sender
        {
            get { return getValue("sender"); }
        }
    }
}