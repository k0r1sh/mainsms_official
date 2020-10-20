using MainSms.Libs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainSms
{
    public class SmsSender
    {
        #region Init
        /// <summary>
        /// Конструктор класса SmsSender
        /// </summary>
        /// <param name="_project">Название проекта</param>
        /// <param name="_api_key">Ключ проекта</param>
        public SmsSender(string _project, string _api_key)
        {
            Settings.project = _project;
            Settings.api_key = _api_key;
        }


        /// <summary>
        /// Конструктор класса SmsSender
        /// </summary>
        /// <param name="_project">Название проекта</param>
        /// <param name="_api_key">Ключ проекта</param>
        /// <param name="_use_ssl">Использовать протокол https</param>
        public SmsSender(string _project, string _api_key, bool _use_ssl)
        {
            Settings.project = _project;
            Settings.api_key = _api_key;
            Settings.use_ssl = _use_ssl;
        }
        #endregion

        #region Sender

        #region Create
        /// <summary>
        /// Добавление отправителя
        /// </summary>
        /// <param name="name">Имя отправителя</param>
        /// <returns></returns>
        public ResponseSenderCreate createSender(string name)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "name", name }
            };
            string response = RequestHelper.post("sender_create", queryParams).Result;
            return new ResponseSenderCreate(response);
        }
        #endregion

        #region Remove
        /// <summary>
        /// Удаление отправителя
        /// </summary>
        /// <param name="name">Имя отправителя</param>
        /// <returns></returns>
        public ResponseSenderRemove removeSender(string name)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "name", name }
            };
            string response = RequestHelper.post("sender_remove", queryParams).Result;
            return new ResponseSenderRemove(response);
        }
        #endregion

        #region List
        /// <summary>
        /// Запрос списка отправителей
        /// </summary>
        /// <returns></returns>
        public ResponseSenderList listSender()
        {
            string response = RequestHelper.post("sender_list").Result;
            return new ResponseSenderList(response);
        }
        #endregion

        #region Default
        /// <summary>
        /// Запрос имени отправителя по умолчанию
        /// </summary>
        /// <returns></returns>
        public ResponseSenderDefault defaultSender()
        {
            string response = RequestHelper.post("sender_default").Result;
            return new ResponseSenderDefault(response);
        }
        #endregion

        #region Result
        /// <summary>
        /// Запрос на установку имени отправителя по умолчанию
        /// </summary>
        /// <param name="name">Имя отправителя</param>
        /// <returns></returns>
        public ResponseSenderSet setSender(string name)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "name", name }
            };
            string response = RequestHelper.post("sender_set", queryParams).Result;
            return new ResponseSenderSet(response);
        }
        #endregion

        #endregion
    }
}
