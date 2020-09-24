using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MainSms
{
    public class ResponseStatus
    {
        /// <summary>
        /// Код ошибки
        /// </summary>
        public string error = "";
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string message = "";
        /// <summary>
        /// Словарь соответствий id сообщения статусу доставки
        /// </summary>
        public Dictionary<string, string> messages = new Dictionary<string, string>();
        /// <summary>
        /// Ответ сервера в формате xml
        /// </summary>
        public string response;
        /// <summary>
        /// Ответ сервера success - успех, error - неудача
        /// </summary>
        public string status;

        public ResponseStatus(string _response)
        {
            try
            {
                response = _response;
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(response);
                if (xd.GetElementsByTagName("status")[0].FirstChild.Value == "success")
                {
                    status = "success";
                    foreach (XmlNode node in xd.GetElementsByTagName("messages")[0].ChildNodes)
                    {
                        messages[node.Name.Replace("id", "")] = node.ChildNodes[0].Value;
                    }
                }
                else
                {
                    status = "error";
                    if (xd.GetElementsByTagName("error").Count == 0 || xd.GetElementsByTagName("message").Count == 0)
                    {
                        message = "Неизвестная ошибка, возможно проблемы с соединением.";
                    }
                    else
                    {
                        error = xd.GetElementsByTagName("error")[0].FirstChild.Value;
                        message = xd.GetElementsByTagName("message")[0].FirstChild.Value;
                    }
                }
            }
            catch { message = "Неизвестная ошибка, возможно проблемы с соединением."; error = "-1"; }
        }
    }
}
