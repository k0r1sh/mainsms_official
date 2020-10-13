using MainSms.Libs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainSms
{
    public class SmsContact
    {
        #region Init
        /// <summary>
        /// Конструктор класса SmsContact
        /// </summary>
        /// <param name="_project">Название проекта</param>
        /// <param name="_api_key">Ключ проекта</param>
        public SmsContact(string _project, string _api_key)
        {
            Settings.project = _project;
            Settings.api_key = _api_key;
        }


        /// <summary>
        /// Конструктор класса SmsContact
        /// </summary>
        /// <param name="_project">Название проекта</param>
        /// <param name="_api_key">Ключ проекта</param>
        /// <param name="_use_ssl">Использовать протокол https</param>
        public SmsContact(string _project, string _api_key, bool _use_ssl)
        {
            Settings.project = _project;
            Settings.api_key = _api_key;
            Settings.use_ssl = _use_ssl;
        }
        #endregion

        #region Contact

        #region Create
        /// <summary>
        /// Создание контакта
        /// </summary>
        /// <param name="recipientForCreate">Данные о создаваемом получателе</param>
        /// <returns></returns>
        public ResponseContactCreate createContact(ContactInfo contactInfo)
        {
            string response = RequestHelper.post("contact_create", contactInfo.toDictionary()).Result;
            return new ResponseContactCreate(response);
        }
        #endregion

        #region Remove
        /// <summary>
        /// Удаление контакта
        /// </summary>
        /// <param name="phone">Номер телефона контакта</param>
        /// <returns></returns>
        public ResponseContactRemove removeContact(string phone)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "phone", phone }
            };
            string response = RequestHelper.post("contact_remove", queryParams).Result;
            return new ResponseContactRemove(response);
        }
        #endregion

        #region Exists
        /// <summary>
        /// Проверка существования контакта
        /// </summary>
        /// <param name="phone">Номер телефона контакта</param>
        /// <returns></returns>
        public ResponseContactExists existsContact(string phone)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "phone", phone }
            };
            string response = RequestHelper.post("contact_exists", queryParams).Result;
            return new ResponseContactExists(response);
        }
        #endregion
        #endregion
    }
}
