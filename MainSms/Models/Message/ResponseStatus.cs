using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace MainSms
{
    public class ResponseStatus : Response
    {
        public ResponseStatus(string data) : base(data) { }

        private Dictionary<string, string> _messages = new Dictionary<string, string>();
        /// <summary>
        /// Статус сообщений (идентификатор => статус)
        /// </summary>
        public Dictionary<string, string> messages
        {
            get { return _messages; }
        }

        private Dictionary<string, string> _channels = new Dictionary<string, string>();
        /// <summary>
        /// Канал доставки сообщений (идентификатор => канал), значения: sms или viber
        /// </summary>
        public Dictionary<string, string> channels
        {
            get { return _channels; }
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
                case "channels":
                    foreach (var element in arrayElement.Elements())
                    {
                        _channels.Add(element.Name.ToString().Replace("id", ""), element.Value);
                    }
                    break;
            }
        }
    }
}