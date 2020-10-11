using System;
using System.Collections.Generic;
using System.Text;

namespace MainSms
{
    /// <summary>
    /// Данные создаваемого получателя
    /// </summary>
    public struct ContactInfo
    {
        /// <summary>
        /// Номер телефона в формате 79*********
        /// </summary>
        public string phone;
        /// <summary>
        /// ID группы, если не задать то добавится только в группу "Все"
        /// </summary>
        public string group;
        /// <summary>
        /// Фамилия
        /// </summary>
        public string lastname;
        /// <summary>
        /// Имя
        /// </summary>
        public string firstname;
        /// <summary>
        /// Отчество
        /// </summary>
        public string patronymic;
        /// <summary>
        /// День рождения в фотмате 29.01.1987
        /// </summary>
        public string birthday;
        /// <summary>
        /// Параметр 1
        /// </summary>
        public string param1;
        /// <summary>
        /// Параметр 2
        /// </summary>
        public string param2;

        public Dictionary<string, string> toDictionary()
        {
            Dictionary<string, string> resultDctionary = new Dictionary<string, string>()
            {
                { "phone", phone },
                { "group", group },
                { "lastname", lastname },
                { "firstname", firstname },
                { "patronymic", patronymic },
                { "birthday", birthday },
                { "param1", param1 },
                { "param2", param2 }
            };
            return resultDctionary;
        }
    }
}
