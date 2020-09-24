using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Xml;
using System.Web;

namespace MainSms
{
    public class Sms
    {
        private string project;
        private string api_key;
        private bool use_ssl;
        private bool is_test;
        private string api_url;
        private string response_type = "xml"; //set xml or json
        private const string REQUEST_SUCCESS = "success";
        private const string REQUEST_ERROR = "error";
        private DateTime null_date = new DateTime();
        private WebProxy proxy = null;
        private string apiDomain = "mainsms.ru";

        /// <summary>
        /// Установка параметров proxy
        /// </summary>
        /// <param name="address">Адрес прокси</param>
        /// <param name="username">Имя пользвателя, если не задано оставьте пустым</param>
        /// <param name="password">Пароль если есть, если не задан оставьте пустым</param>
        public void setProxy(Uri address, string username, string password)
        {
            proxy = new WebProxy(address);
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                proxy.Credentials = new NetworkCredential(username, password);
            }
        }

        private static string GetHash(HashAlgorithm hashString, string text)
        {
            byte[] hashValue;
            byte[] message = Encoding.UTF8.GetBytes(text);
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        private string fetch(string url, string data)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                string postData = data;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                if (null != proxy) { request.Proxy = proxy;} 
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                return responseFromServer;
            }
            catch { return ""; }
        }
        /// <summary>
        /// Конструктор класса Sms
        /// </summary>
        /// <param name="_project">Название проекта</param>
        /// <param name="_api_key">Ключ проекта</param>
        public Sms(string _project, string _api_key)
        {
            project = _project;
            api_key = _api_key;
            use_ssl = false;
            is_test = false;
            api_url = use_ssl ? String.Format("https://" + (apiDomain.Equals("mainsms.ru") ? "mainsms.ru/api/mainsms" : ("api." + apiDomain)) + "/") : String.Format("http://" + (apiDomain.Equals("mainsms.ru") ? "mainsms.ru/api/mainsms" : ("api." + apiDomain)) + "/");
        }

        /// <summary>
        /// Конструктор класса Sms
        /// </summary>
        /// <param name="_project">Название проекта</param>
        /// <param name="_api_key">Ключ проекта</param>
        /// <param name="_is_test">Используется для отладки</param>
        public Sms(string _project, string _api_key, bool _is_test)
        {
            project = _project;
            api_key = _api_key;
            use_ssl = false;
            is_test = _is_test;
            api_url = use_ssl ? String.Format("https://" + (apiDomain.Equals("mainsms.ru") ? "mainsms.ru/api/mainsms" : ("api." + apiDomain)) + "/") : String.Format("http://" + (apiDomain.Equals("mainsms.ru") ? "mainsms.ru/api/mainsms" : ("api." + apiDomain)) + "/");
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
            project = _project;
            api_key = _api_key;
            use_ssl = _use_ssl;
            is_test = _is_test;
            api_url = use_ssl ? String.Format("https://" + (apiDomain.Equals("mainsms.ru") ? "mainsms.ru/api/mainsms" : ("api." + apiDomain)) + "/") : String.Format("http://" + (apiDomain.Equals("mainsms.ru") ? "mainsms.ru/api/mainsms" : ("api." + apiDomain)) + "/");
        }

        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="sender">Имя отправителя</param>
        /// <param name="recipients">Номера получателей в любом формате через запятую</param>
        /// <param name="message">Текст сообщения</param>
        /// <returns></returns>
        public ResponseSend send(string sender, string recipients, string message)
        {
            return send(sender, recipients, message, null_date);
        }

        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="sender">Имя отправителя</param>
        /// <param name="recipients">Номера получателей в любом формате через запятую</param>
        /// <param name="message">Текст сообщения</param>
        /// <param name="run_at">Время доставки сообщения, укажите если сообщение должно быть доставленно в определенное время.</param>
        /// <returns></returns>
        public ResponseSend send(string sender, string recipients, string message, DateTime run_at)
        {
            string encoded_sender = HttpUtility.UrlEncode(sender);
            string encoded_recipients = HttpUtility.UrlEncode(recipients);
            string encoded_message = HttpUtility.UrlEncode(message);
            string data = "project=" + project + "&sender=" + encoded_sender + "&message=" + encoded_message + "&recipients=" + encoded_recipients + "&test=" + (is_test ? "1" : "0") + "&format=" + response_type;
            string to_hash = project + ";" + sender + ";" + message + ";" + recipients + ";" + (is_test ? "1" : "0") + ";" + response_type + ";";
            to_hash += (null_date == run_at) ? api_key : (run_at.ToString("d.M.yyyy H:m") + ";" + api_key);
            string sign = GetHash(new MD5CryptoServiceProvider(), GetHash(new SHA1Managed(), to_hash));
            data += (null_date == run_at) ? ("&sign=" + sign) : ("&run_at=" + run_at.ToString("d.M.yyyy H:m") + "&sign=" + sign);
            return new ResponseSend(fetch(api_url + "message/send", data));
        }

        /// <summary>
        /// Запрос стоимости сообщения
        /// </summary>
        /// <param name="sender">Имя отправителя</param>
        /// <param name="recipients">Номера получателей в любом формате через запятую</param>
        /// <param name="message">Текст сообщения</param>
        /// <returns></returns>
        public ResponsePrice getMessagesPrice(string sender,string recipients, string message)
        {
            string encoded_sender = HttpUtility.UrlEncode(sender);
            string encoded_recipients = HttpUtility.UrlEncode(recipients);
            string encoded_message = HttpUtility.UrlEncode(message);
            string data = "project=" + project + "&message=" + encoded_message + "&recipients=" + encoded_recipients + "&format=" + response_type +"&sender="+ encoded_sender + "&sign=" + GetHash(new MD5CryptoServiceProvider(), GetHash(new SHA1Managed(), project + ";" + message + ";" + recipients + ";" + response_type + ";"+sender+";" + api_key));
            return new ResponsePrice(fetch(api_url + "message/price", data));
        }

        /// <summary>
        /// Запрос баланса
        /// </summary>
        /// <returns></returns>
        public ResponseBalance getBalance()
        {
            string data = "project=" + project + "&format=" + response_type + "&sign=" + GetHash(new MD5CryptoServiceProvider(), GetHash(new SHA1Managed(), project + ";" + response_type + ";" + api_key));
            return new ResponseBalance(fetch(api_url + "message/balance", data));
        }

        /// <summary>
        /// Запрос статуса сообщений
        /// </summary>
        /// <param name="messages_id">Статусы сообщений через запятую</param>
        /// <returns></returns>
        public ResponseStatus getMessagesStatus(string messages_id)
        {
            string encoded_messages_id = HttpUtility.UrlEncode(messages_id);
            string data = "project=" + project + "&messages_id=" + encoded_messages_id + "&format=" + response_type + "&sign=" + GetHash(new MD5CryptoServiceProvider(), GetHash(new SHA1Managed(), project + ";" + messages_id + ";" + response_type + ";" + api_key));
            return new ResponseStatus(fetch(api_url + "message/status", data));
        }

        /// <summary>
        /// Информация о номерах
        /// </summary>
        /// <param name="phones">Номера в любом формате, через запятую</param>
        /// <returns></returns>
        public ResponseInfo getPhonesInfo(string phones)
        {
            string encoded_phones = HttpUtility.UrlEncode(phones);
            string data = "project=" + project + "&phones=" + encoded_phones + "&format=" + response_type + "&sign=" + GetHash(new MD5CryptoServiceProvider(), GetHash(new SHA1Managed(), project + ";" + phones + ";" + response_type + ";" + api_key));
            return new ResponseInfo(fetch(api_url + "message/info", data));
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
