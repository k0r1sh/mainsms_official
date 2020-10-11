using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MainSms
{
    public class ResponseContactRemove : Response
    {
        /// <summary>
        /// Ответ удаление контакта
        /// </summary>
        public ResponseContactRemove(string data) : base(data) { }

        private List<string> _phones = new List<string>();
        /// <summary>
        /// Массив номеров
        /// </summary>
        public List<string> phones
        {
            get { return _phones; }
        }

        protected override void storeArray(XElement arrayElement)
        {
            switch (arrayElement.Name.ToString())
            {
                case "phones":
                    foreach (var element in arrayElement.Elements())
                    {
                        _phones.Add(element.Value);
                    }
                    break;
            }
        }
    }
}