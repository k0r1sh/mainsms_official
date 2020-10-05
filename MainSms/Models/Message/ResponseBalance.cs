using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace MainSms
{
    /// <summary>
    /// Ответ на запрос баланса
    /// </summary>
    public class ResponseBalance : Response
    {
        public ResponseBalance(string data) : base(data) { }

        /// <summary>
        /// Остаток на счете
        /// </summary>
        public string balance
        {
            get { return getValue("balance"); }
        }

    }
}
