using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Xml;
using MainSms.Libs;

namespace MainSms
{
    public class Sms
    {
        private DateTime null_date = new DateTime();

        #region Init
        /// <summary>
        /// Конструктор класса Sms
        /// </summary>
        /// <param name="_project">Название проекта</param>
        /// <param name="_api_key">Ключ проекта</param>
        public Sms(string _project, string _api_key)
        {
            Settings.project = _project;
            Settings.api_key = _api_key;
        }

        /// <summary>
        /// Конструктор класса Sms
        /// </summary>
        /// <param name="_project">Название проекта</param>
        /// <param name="_api_key">Ключ проекта</param>
        /// <param name="_is_test">Используется для отладки</param>
        public Sms(string _project, string _api_key, bool _is_test)
        {
            Settings.project = _project;
            Settings.api_key = _api_key;
            Settings.is_test = _is_test;
        }

        /// <summary>
        /// Конструктор класса Sms
        /// </summary>
        /// <param name="_project">Название проекта</param>
        /// <param name="_api_key">Ключ проекта</param>
        /// <param name="_is_test">Используется для отладки</param>
        /// <param name="_use_ssl">Использовать протокол https</param>
        public Sms(string _project, string _api_key, bool _is_test, bool _use_ssl)
        {
            Settings.project = _project;
            Settings.api_key = _api_key;
            Settings.use_ssl = _use_ssl;
            Settings.is_test = _is_test;
        }
        #endregion

        #region Message
        #region Send
        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="sender">Имя отправителя</param>
        /// <param name="recipients">Номера получателей в любом формате через запятую</param>
        /// <param name="message">Текст сообщения</param>
        /// <returns></returns>
        public ResponseSend sendSms(string sender, string recipients, string message)
        {
            return sendSms(sender, recipients, message, null_date);
        }

        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="sender">Имя отправителя</param>
        /// <param name="recipients">Номера получателей в любом формате через запятую</param>
        /// <param name="message">Текст сообщения</param>
        /// <param name="run_at">Время доставки сообщения в часовом поясе вашего аккаунта, укажите если сообщение должно быть доставленно в определенное время.</param>
        /// <returns></returns>
        public ResponseSend sendSms(string sender, string recipients, string message, DateTime run_at)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "sender", sender },
                { "recipients", recipients },
                { "message", message}
            };
            if (Settings.is_test) queryParams.Add("test", "1");
            if (run_at != null_date) queryParams.Add("run_at", run_at.ToString("d.M.yyyy H:m"));

            string response = RequestHelper.post("message_send", queryParams).Result;
            return new ResponseSend(response);
        }

        /// <summary>
        /// Отправка VIBER сообщения
        /// </summary>
        /// <param name="sender">Имя отправителя</param>
        /// <param name="recipients">Номера получателей в любом формате через запятую</param>
        /// <param name="viber_text">Текст VIBER сообщения</param>
        /// <param name="image">URL изображения</param>
        /// <param name="button">Текст на кнопке</param>
        /// <param name="button_url">Ссылка для кнопки</param>
        /// <returns></returns>
        public ResponseSend sendViber(string sender, string recipients, string viber_text, string image, string button, string button_url)
        {
            return sendViber(sender, recipients, viber_text, image, button, button_url, null_date);
        }

        /// <summary>
        /// Отправка VIBER сообщения
        /// </summary>
        /// <param name="sender">Имя отправителя</param>
        /// <param name="recipients">Номера получателей в любом формате через запятую</param>
        /// <param name="viber_text">Текст VIBER сообщения</param>
        /// <param name="image">URL изображения</param>
        /// <param name="button">Текст на кнопке</param>
        /// <param name="button_url">Ссылка для кнопки</param>
        /// <param name="run_at">Время доставки сообщения в часовом поясе вашего аккаунта, укажите если сообщение должно быть доставленно в определенное время.</param>
        /// <returns></returns>
        public ResponseSend sendViber(string sender, string recipients, string viber_text, string image, string button, string button_url, DateTime run_at)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "sender", sender },
                { "recipients", recipients },
                { "viber_text", viber_text},
                { "image", image},
                { "button", button},
                { "button_url", button_url},
                { "message", "empty" },
                { "viber", "1" }
            };
            if (Settings.is_test) queryParams.Add("test", "1");
            if (run_at != null_date) queryParams.Add("run_at", run_at.ToString("d.M.yyyy H:m"));

            string response = RequestHelper.post("message_send", queryParams).Result;
            return new ResponseSend(response);
        }

        /// <summary>
        /// Отправка "Viber вместо SMS" сообщения
        /// </summary>
        /// <param name="sender">Имя отправителя</param>
        /// <param name="recipients">Номера получателей в любом формате через запятую</param>
        /// <param name="viber_text">Текст VIBER сообщения</param>
        /// <param name="image">URL изображения</param>
        /// <param name="button">Текст на кнопке</param>
        /// <param name="button_url">Ссылка для кнопки</param>
        /// <returns></returns>
        public ResponseSend sendViberOrSms(string sender, string recipients, string message, string viber_text, string image, string button, string button_url)
        {
            return sendViberOrSms(sender, recipients, message, viber_text, image, button, button_url, null_date);
        }

        /// <summary>
        /// Отправка "Viber вместо SMS" сообщения
        /// </summary>
        /// <param name="sender">Имя отправителя</param>
        /// <param name="recipients">Номера получателей в любом формате через запятую</param>
        /// <param name="viber_text">Текст VIBER сообщения</param>
        /// <param name="image">URL изображения</param>
        /// <param name="button">Текст на кнопке</param>
        /// <param name="button_url">Ссылка для кнопки</param>
        /// <param name="run_at">Время доставки сообщения в часовом поясе вашего аккаунта, укажите если сообщение должно быть доставленно в определенное время.</param>
        /// <returns></returns>
        public ResponseSend sendViberOrSms(string sender, string recipients, string message, string viber_text, string image, string button, string button_url, DateTime run_at)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "sender", sender },
                { "recipients", recipients },
                { "viber_text", viber_text},
                { "image", image},
                { "button", button},
                { "button_url", button_url},
                { "message", message },
                { "viber", "2" }
            };
            if (Settings.is_test) queryParams.Add("test", "1");
            if (run_at != null_date) queryParams.Add("run_at", run_at.ToString("d.M.yyyy H:m"));

            string response = RequestHelper.post("message_send", queryParams).Result;
            return new ResponseSend(response);
        }
        #endregion

        #region MessageStatus
        /// <summary>
        /// Запрос статуса сообщений
        /// </summary>
        /// <param name="messages_id">Статусы сообщений через запятую</param>
        /// <returns></returns>
        public ResponseStatus getMessagesStatus(string messages_id)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "messages_id", messages_id }
            };
            string response = RequestHelper.post("message_status", queryParams).Result;
            return new ResponseStatus(response);
        }
        #endregion

        #region MessageCancel
        /// <summary>
        /// Отмена запланированных сообщений
        /// </summary>
        /// <param name="messages_id">Статусы сообщений через запятую</param>
        /// <returns></returns>
        public ResponseCancel cancelMessages(string messages_id)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "messages_id", messages_id }
            };
            string response = RequestHelper.post("message_cancel", queryParams).Result;
            return new ResponseCancel(response);
        }
        #endregion

        #region MessagePrice
        /// <summary>
        /// Запрос стоимости сообщения
        /// </summary>
        /// <param name="sender">Имя отправителя</param>
        /// <param name="recipients">Номера получателей в любом формате через запятую</param>
        /// <param name="message">Текст сообщения</param>
        /// <returns></returns>
        public ResponsePrice getMessagesPrice(string sender,string recipients, string message)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "sender", sender },
                { "recipients", recipients },
                { "message", message}
            };
            string response = RequestHelper.post("message_price", queryParams).Result;
            return new ResponsePrice(response);
        }
        #endregion

        #region Balance
        /// <summary>
        /// Запрос баланса
        /// </summary>
        /// <returns></returns>
        public ResponseBalance getBalance()
        {
            string response = RequestHelper.post("message_balance").Result;
            return new ResponseBalance(response);
        }
        #endregion

        #region PhoneInfo
        /// <summary>
        /// Информация о номерах
        /// </summary>
        /// <param name="phones">Номера в любом формате, через запятую</param>
        /// <returns></returns>
        public ResponseInfo getPhonesInfo(string phones)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "phones", phones }
            };
            string response = RequestHelper.post("message_info", queryParams).Result;
            return new ResponseInfo(response);
        }
        #endregion
        #endregion

    }
}
