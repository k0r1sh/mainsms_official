using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MainSms
{
    public class ResponseSendingStatus : Response
    {
        /// <summary>
        /// Ответ создание запрос статуса рассылки
        /// </summary>
        public ResponseSendingStatus(string data) : base(data) { }
        public override string status
        {
            get { return variables.ContainsKey("id") ? "success" : "error" ; }
        }
        /// <summary>
        /// id рассылки
        /// </summary>
        public string id
        {
            get { return getValue("id"); }
        }
        /// <summary>
        /// Всего контактов получателей
        /// </summary>
        public string total
        {
            get { return getValue("total"); }
        }
        /// <summary>
        /// Количество доставленых смс
        /// </summary>
        public string delivered
        {
            get { return getValue("delivered"); }
        }
        /// <summary>
        /// Количество не доставленых смс
        /// </summary>
        public string undelivered
        {
            get { return getValue("undelivered"); }
        }
        /// <summary>
        /// Количество смс в статусе "Отправлено" (статус ещё не вернулся от оператора)
        /// </summary>
        public string indelivered
        {
            get { return getValue("indelivered"); }
        }
    }
}