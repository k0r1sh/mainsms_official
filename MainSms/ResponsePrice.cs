﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MainSms
{
    /// <summary>
    /// Ответ на запрос стоимости
    /// </summary>
    public class ResponsePrice
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
        /// Остаток на счете
        /// </summary>
        public string balance = "";
        /// <summary>
        /// Список номеров получателей в формате 79*********
        /// </summary>
        public List<string> recipients = new List<string>();
        /// <summary>
        /// Количество отправленных смс
        /// </summary>
        public string count = "";
        /// <summary>
        /// Количество частей из которых состоит сообщение
        /// </summary>
        public string parts = "";
        /// <summary>
        /// Стоимость отправки
        /// </summary>
        public string price = "";

        public ResponsePrice(string _response)
        {
            try
            {
                response = _response;
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(response);
                if (xd.GetElementsByTagName("status")[0].FirstChild.Value == "success")
                {
                    status = "success";
                    balance = xd.GetElementsByTagName("balance")[0].FirstChild.Value;
                    price = xd.GetElementsByTagName("price")[0].FirstChild.Value;
                    parts = xd.GetElementsByTagName("parts")[0].FirstChild.Value;
                    count = xd.GetElementsByTagName("count")[0].FirstChild.Value;
                    foreach (XmlNode node in xd.GetElementsByTagName("recipients")[0].ChildNodes)
                    {
                        recipients.Add(node.ChildNodes[0].Value);
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
