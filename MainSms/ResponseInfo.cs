using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MainSms
{
    /// <summary>
    /// Ответ на запрос информации по номерам
    /// </summary>
    public class ResponseInfo
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
        /// Ответ сервера в формате xml
        /// </summary>
        public string response;
        /// <summary>
        /// Ответ сервера success - успех, error - неудача
        /// </summary>
        public string status;
        /// <summary>
        /// Список ответов по запрашиваемым номерам
        /// </summary>
        public List<PhoneInfo> info = new List<PhoneInfo>();

        public ResponseInfo(string _response)
        {
            try
            {
                response = _response;
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(response);
                if (xd.GetElementsByTagName("status")[0].FirstChild.Value == "success")
                {
                    status = "success";
                    foreach (XmlNode node in xd.GetElementsByTagName("info")[0].ChildNodes)
                    {
                        PhoneInfo phone_info = new PhoneInfo();
                        foreach (XmlNode info_node in node.ChildNodes)
                        {
                            if (info_node.Name == "region") phone_info.region = info_node.ChildNodes[0].Value;
                            if (info_node.Name == "phone") phone_info.phone = info_node.ChildNodes[0].Value;
                            if (info_node.Name == "code") phone_info.code = info_node.ChildNodes[0].Value;
                            if (info_node.Name == "name") phone_info.name = info_node.ChildNodes[0].Value;
                        }
                        //phone_info.name = node.ChildNodes[0].Value;
                        info.Add(phone_info);
                    }
                }
                else
                {
                    status = "error";
                    if (xd.GetElementsByTagName("error").Count == 0 || xd.GetElementsByTagName("message").Count == 0)
                    {
                        message = "Неизвестная ошибка, возможно проблемы с соединением.";
                        error = "-1";
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
