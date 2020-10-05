using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace MainSms
{
    /// <summary>
    /// Ответ на запрос информации по номерам
    /// </summary>
    public class ResponseInfo : Response
    {
        public ResponseInfo(string data) : base(data) { }

        private List<PhoneInfo> _info = new List<PhoneInfo>();
        /// <summary>
        /// Список ответов по запрашиваемым номерам
        /// </summary>
        public List<PhoneInfo> info
        {
            get { return _info; }
        }

        protected override void storeArray(XElement arrayElement)
        {
            switch (arrayElement.Name.ToString())
            {
                case "info":
                    foreach (var element in arrayElement.Elements())
                    {
                        PhoneInfo phone_info = new PhoneInfo();
                        foreach(var phone_attr_element in element.Elements())
                        {
                            switch (phone_attr_element.Name.ToString())
                            {
                                case "region":
                                    phone_info.region = phone_attr_element.Value;
                                    break;
                                case "phone":
                                    phone_info.phone = phone_attr_element.Value;
                                    break;
                                case "code":
                                    phone_info.code = phone_attr_element.Value;
                                    break;
                                case "name":
                                    phone_info.name = phone_attr_element.Value;
                                    break;
                            }
                        }
                        _info.Add(phone_info);
                    }
                    break;
            }
        }

        /// <summary>
        /// Информация о номере телефона
        /// </summary>
        public struct PhoneInfo
        {
            /// <summary>
            /// Номер телефона в формате 79*********
            /// </summary>
            public string phone;
            /// <summary>
            /// Код оператора
            /// </summary>
            public string code;
            /// <summary>
            /// Регион
            /// </summary>
            public string region;
            /// <summary>
            /// Название оператора
            /// </summary>
            public string name;
        }
    }
}
