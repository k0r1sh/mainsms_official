using MainSms.Libs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainSms
{
    public class SmsSending
    {
        #region Init
        /// <summary>
        /// Конструктор класса SmsSending
        /// </summary>
        /// <param name="_project">Название проекта</param>
        /// <param name="_api_key">Ключ проекта</param>
        public SmsSending(string _project, string _api_key)
        {
            Settings.project = _project;
            Settings.api_key = _api_key;
        }


        /// <summary>
        /// Конструктор класса SmsSending
        /// </summary>
        /// <param name="_project">Название проекта</param>
        /// <param name="_api_key">Ключ проекта</param>
        /// <param name="_use_ssl">Использовать протокол https</param>
        public SmsSending(string _project, string _api_key, bool _is_test)
        {
            Settings.project = _project;
            Settings.api_key = _api_key;
            Settings.is_test = _is_test;
        }
        /// <summary>
        /// Конструктор класса SmsSending
        /// </summary>
        /// <param name="_project">Название проекта</param>
        /// <param name="_api_key">Ключ проекта</param>
        /// <param name="_is_test">Используется для отладки</param>
        /// <param name="_use_ssl">Использовать протокол https</param>
        public SmsSending(string _project, string _api_key, bool _is_test, bool _use_ssl)
        {
            Settings.project = _project;
            Settings.api_key = _api_key;
            Settings.use_ssl = _use_ssl;
            Settings.is_test = _is_test;
        }
        #endregion

        #region Sending

        #region Create
        /// <summary>
        /// Создание рассылки
        /// </summary>
        /// <param name="sendingInfo">Данные о создаваемой рассылке</param>
        /// <returns></returns>
        public ResponseSendingCreate createSending(SendingInfo sendingInfo)
        {
            Dictionary<string, string> queryParams = sendingInfo.toDictionary();
            if (Settings.is_test) queryParams.Add("test", "1");
            string response = RequestHelper.post("sending_create", queryParams).Result;
            return new ResponseSendingCreate(response);
        }
        #endregion

        #region Status
        /// <summary>
        /// Запрос статуса рассылки
        /// </summary>
        /// <param name="id">id рассылки</param>
        /// <returns></returns>
        public ResponseSendingStatus sendingStatus(string id)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "id", id }
            };
            string response = RequestHelper.post("sending_status", queryParams).Result;
            return new ResponseSendingStatus(response);
        }
        #endregion

        #endregion
    }
}
