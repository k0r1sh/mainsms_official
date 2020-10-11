using System;
using System.Collections.Generic;
using System.Text;

namespace MainSms
{
    public class ResponseGroupRemove : Response
    {
        /// <summary>
        /// Ответ на удаление группы получателей
        /// </summary>
        public ResponseGroupRemove(string data) : base(data) { }

        /// <summary>
        /// Результат удаления
        /// </summary>
        public string result
        {
            get { return getValue("result"); }
        }
    }
}