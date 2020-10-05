using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace MainSms
{
    /// <summary>
    /// Ответ на запрос стоимости
    /// </summary>
    public class ResponsePrice : Response
    {
        public ResponsePrice(string data) : base(data) { }

        /// <summary>
        /// Остаток на счете
        /// </summary>
        public string balance
        {
            get { return getValue("balance"); }
        }

        private List<string> _recipients = new List<string>();
        /// <summary>
        /// Список номеров получателей в формате 79*********
        /// </summary>
        /// 
        public List<string> recipients
        {
            get { return _recipients; }
        }
        /// <summary>
        /// Количество отправленных смс
        /// </summary>
        public string count
        {
            get { return getValue("count"); }
        }
        /// <summary>
        /// Количество частей из которых состоит сообщение
        /// </summary>
        public string parts
        {
            get { return getValue("parts"); }
        }
        /// <summary>
        /// Стоимость отправки
        /// </summary>
        public string price
        {
            get { return getValue("price"); }
        }

        protected override void storeArray(XElement arrayElement)
        {
            switch (arrayElement.Name.ToString())
            {
                case "recipients":
                    foreach (var element in arrayElement.Elements())
                    {
                        _recipients.Add(element.Value);
                    }
                    break;
            }
        }
    }
}
