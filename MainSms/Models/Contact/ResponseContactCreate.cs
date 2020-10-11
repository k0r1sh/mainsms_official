using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MainSms
{
    public class ResponseContactCreate : Response
    {
        /// <summary>
        /// Ответ создание получателя
        /// </summary>
        public ResponseContactCreate(string data) : base(data) { }

        private List<string> _phones = new List<string>();
        /// <summary>
        /// Массив номеров
        /// </summary>
        public List<string> phones
        {
            get { return _phones; }
        }

        private List<string> _group = new List<string>();
        /// <summary>
        /// Массив id групп
        /// </summary>
        public List<string> groups
        {
            get { return _group; }
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
                case "group":
                    foreach (var element in arrayElement.Elements())
                    {
                        _group.Add(element.Value);
                    }
                    break;
            }
        }
    }
}