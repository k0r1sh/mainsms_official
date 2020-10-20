using System;
using System.Collections.Generic;
using System.Text;

namespace MainSms
{
    public class ResponseSenderDefault : Response
    {
        /// <summary>
        /// Ответ на запрос имени по умолчанию
        /// </summary>
        public ResponseSenderDefault(string data) : base(data) { }
        public override string status
        {
            get { return variables.ContainsKey("sender") ? "success" : "error"; }
        }
        /// <summary>
        /// Имя отправителя
        /// </summary>
        public string sender
        {
            get { return getValue("sender"); }
        }
    }
}