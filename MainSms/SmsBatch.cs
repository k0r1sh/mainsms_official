using MainSms.Libs;
using MainSms;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainSms
{
    public class SmsBatch
    {
        #region Init
        /// <summary>
        /// Конструктор класса SmsBatch
        /// </summary>
        /// <param name="_project">Название проекта</param>
        /// <param name="_api_key">Ключ проекта</param>
        public SmsBatch(string _project, string _api_key)
        {
            Settings.project = _project;
            Settings.api_key = _api_key;
        }


        /// <summary>
        /// Конструктор класса SmsBatch
        /// </summary>
        /// <param name="_project">Название проекта</param>
        /// <param name="_api_key">Ключ проекта</param>
        /// <param name="_use_ssl">Использовать протокол https</param>
        public SmsBatch(string _project, string _api_key, bool _use_ssl)
        {
            Settings.project = _project;
            Settings.api_key = _api_key;
            Settings.use_ssl = _use_ssl;
        }
        #endregion

        #region Batch

        #region Send
        /// <summary>
        /// Отправка сообщений
        /// </summary>
        /// <param name="recipientForCreate">Данные о создаваемом получателе</param>
        /// <returns></returns>
        public ResponseBatchSend sendBatch(BatchMessagesList batchMessagesList)
        {
            string response = RequestHelper.postBatch("batch_send", batchMessagesList.toDictionary(), batchMessagesList.toSignDictionary()).Result;
            return new ResponseBatchSend(response);
        }
        #endregion

        #endregion
    }
}