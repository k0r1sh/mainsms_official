using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MainSms
{
    public class ResponseBatchSend : Response
    {
        /// <summary>
        /// Ответ на пакетную отправку сообещний
        /// </summary>
        public ResponseBatchSend(string data) : base(data) { }

        /// <summary>
        /// Идентификатор запроса
        /// </summary>
        public string id
        {
            get { return getValue("id"); }
        }

        /// <summary>
        /// Количество получателей
        /// </summary>
        public string phones
        {
            get { return getValue("phones"); }
        }

        /// <summary>
        /// Общее количество частей смс
        /// </summary>
        public string parts
        {
            get { return getValue("parts"); }
        }

        /// <summary>
        /// Общая стоимость
        /// </summary>
        public string cost
        {
            get { return getValue("cost"); }
        }

        private Dictionary<string, string> _errors = new Dictionary<string, string>();
        /// <summary>
        /// Список ошибок возникших при отправке сообщений, ID сообщения -> текст ошибки.
        /// </summary>
        public Dictionary<string, string> errors
        {
            get { return _errors; }
        }

        protected override void storeArray(XElement arrayElement)
        {
            switch (arrayElement.Name.ToString())
            {
                case "errors":
                   
                    foreach (var elementError in arrayElement.Elements())
                    {
                        string messageId = "";
                        string messageError = "";
                        foreach (var element in elementError.Elements())
                        {
                            switch (element.Name.ToString())
                            {
                                case "id":
                                    messageId = element.Value;
                                    break;
                                case "messages":
                                    messageError = element.Value;
                                    break;
                            }
                        }
                        if ("" != messageId && "" != messageError)
                        _errors.Add(messageId, messageError);
                    }
                    break;
            }
        }
    }
}