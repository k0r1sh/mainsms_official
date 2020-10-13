using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MainSms
{
    public class ResponseSendingCreate : Response
    {
        /// <summary>
        /// Ответ создание рассылки
        /// </summary>
        public ResponseSendingCreate(string data) : base(data) { }
        /// <summary>
        /// Режим тестирования
        /// </summary>
        public string test
        {
            get { return getValue("test"); }
        }
        /// <summary>
        /// id рассылки
        /// </summary>
        public string id
        {
            get { return getValue("id"); }
        }
        /// <summary>
        /// Стоимость рассылки
        /// </summary>
        public string cost
        {
            get { return getValue("cost"); }
        }
        /// <summary>
        /// Количество частей в сообщении
        /// </summary>
        public string parts 
        {
            get { return getValue("parts"); }
        }
        /// <summary>
        /// Количество получателей
        /// </summary>
        public string contacts 
        {
            get { return getValue("contacts"); }
        }
        /// <summary>
        /// Группы получателей через запятую
        /// </summary>
        public string include
        {
            get { return getValue("include"); }
        }
        /// <summary>
        /// Исключенные группы получателей через запятую
        /// </summary>
        public string exclude
        {
            get { return getValue("exclude"); }
        }
        /// <summary>
        /// Название рассылки
        /// </summary>
        public string name
        {
            get { return getValue("name"); }
        }
    }
}