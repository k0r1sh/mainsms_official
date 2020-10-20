using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MainSms
{
    public class ResponseSenderList : Response
    {
        /// <summary>
        /// Ответ на запрос списка отправителей
        /// </summary>
        public ResponseSenderList(string data) : base(data) { }

        private List<string> _senders = new List<string>();
        /// <summary>
        /// Список имен отправителей
        /// </summary>
        public List<string> senders
        {
            get { return _senders; }
        }

        protected override void storeArray(XElement arrayElement)
        {
            switch (arrayElement.Name.ToString())
            {
                case "senders":
                    foreach (var element in arrayElement.Elements())
                    {
                        _senders.Add(element.Value);
                    }
                    break;
            }
            if (!variables.ContainsKey("status")) variables["status"] = "success";
        }
    }
}