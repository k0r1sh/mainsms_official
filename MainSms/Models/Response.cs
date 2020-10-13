using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MainSms
{
    /// <summary>
    /// Ответ на запрос баланса
    /// </summary>
    public abstract class Response
    {
        /// <summary>
        /// Код ошибки
        /// </summary>
        public string erorr
        {
            get { return getValue("error"); }
        }
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string message
        {
            get { return getValue("message"); }
        }
        /// <summary>
        /// Ответ сервера в формате xml
        /// </summary>
        public string response;
        /// <summary>
        /// Ответ сервера success - успех, error - неудача
        /// </summary>
        public virtual string status
        {
            get { return getValue("status"); }
        }

        protected Response(string data)
        {
            response = data;
            XDocument doc = XDocument.Parse(response);
            foreach (var element in doc.Element("result").Elements())
            {
                if (element.HasElements)
                    storeArray(element);
                
                else
                    variables.Add(element.Name.ToString(), element.Value);
            }
        }

        protected virtual void storeArray(XElement arrayElement) { }

        protected Dictionary<string, string> variables = new Dictionary<string, string>();

        protected string getValue(string name)
        {
            if (variables.ContainsKey(name))
                return variables[name];
            return "";
        }
    }
}
