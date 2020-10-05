using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace MainSms
{
    public class ResponseCancel : Response
    {
        public ResponseCancel(string data) : base(data) { }

        private Dictionary<string, string> _messages = new Dictionary<string, string>();
        /// <summary>
        /// Статус сообщений (идентификатор => статус)
        /// </summary>
        public Dictionary<string, string> messages
        {
            get { return _messages; }
        }

        protected override void storeArray(XElement arrayElement)
        {
            switch (arrayElement.Name.ToString())
            {
                case "messages":
                    foreach (var element in arrayElement.Elements())
                    {
                        _messages.Add(element.Name.ToString().Replace("id", ""), element.Value);
                    }
                    break;
            }
        }
    }
}