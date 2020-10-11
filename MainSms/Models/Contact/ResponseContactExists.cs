using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;

namespace MainSms
{
    public class ResponseContactExists : Response
    {
        /// <summary>
        /// Ответ на проверку существования контакта
        /// </summary>
        public ResponseContactExists(string data) : base(data) { }

        private List<ContactInfo> _contactInfo = new List<ContactInfo>();
        /// <summary>
        /// Массив объектов с информацией о контактах
        /// </summary>
        public List<ContactInfo> contactInfo
        {
            get { return _contactInfo; }
        }

        protected override void storeArray(XElement arrayElement)
        {
            switch (arrayElement.Name.ToString())
            {
                case "result":
                    ContactInfo contactInfo = new ContactInfo();
                    foreach (var element in arrayElement.Elements())
                    {
                        switch (element.Name.ToString())
                        {
                            case "phone":
                                contactInfo.phone = element.Value;
                                break;
                            case "groups":
                                var valuesArray = (from p in element.Elements()
                                                   select (string)p.Value).ToArray();
                                contactInfo.group = String.Join(",", valuesArray);
                                break;
                            case "lastname":
                                contactInfo.lastname = element.Value;
                                break;
                            case "firstname":
                                contactInfo.firstname = element.Value;
                                break;
                            case "patronymic":
                                contactInfo.patronymic = element.Value;
                                break;
                            case "birthday":
                                contactInfo.birthday = element.Value;
                                break;
                            case "param1":
                                contactInfo.param1 = element.Value;
                                break;
                            case "param2":
                                contactInfo.param2 = element.Value;
                                break;
                        }
                    }
                    _contactInfo.Add(contactInfo);
                    break;
            }
            if (!variables.ContainsKey("status")) variables["status"] = "success";
        }
    }
}