using System;
using System.Collections.Generic;
using System.Text;

namespace MainSms
{
    public class ResponseGroupCreate : Response
    {
        /// <summary>
        /// Ответ на создание группы получателей
        /// </summary>
        public ResponseGroupCreate(string data) : base(data) { }

        /// <summary>
        /// Уникальный идентификатор в системе
        /// </summary>
        public string id
        {
            get { return getValue("id"); }
        }

        /// <summary>
        /// Название группы получателей
        /// </summary>
        public string name
        {
            get { return getValue("name"); }
        }
    }
}