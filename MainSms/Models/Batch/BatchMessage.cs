using System;
using System.Collections.Generic;
using System.Text;

namespace MainSms
{
    /// <summary>
    /// Данные создаваемого получателя
    /// </summary>
    public struct BatchMessage
    {
        public BatchMessage(string _id, string _phone, string _text)
        {
            id = _id;
            phone = _phone;
            text = _text;
        }

        /// <summary>
        /// Локальный ID сообщения
        /// </summary>
        public string id;
        /// <summary>
        /// Номер телефона в формате 79*********
        /// </summary>
        public string phone;
        /// <summary>
        /// Тескт сообщения
        /// </summary>
        public string text;
    }
}
