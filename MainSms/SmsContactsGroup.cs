using MainSms.Libs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainSms
{
    public class SmsContactsGroup
    {
        #region Init
        /// <summary>
        /// Конструктор класса SmsRecipientsGroup
        /// </summary>
        /// <param name="_project">Название проекта</param>
        /// <param name="_api_key">Ключ проекта</param>
        public SmsContactsGroup(string _project, string _api_key)
        {
            Settings.project = _project;
            Settings.api_key = _api_key;
        }


        /// <summary>
        /// Конструктор класса SmsRecipientsGroup
        /// </summary>
        /// <param name="_project">Название проекта</param>
        /// <param name="_api_key">Ключ проекта</param>
        /// <param name="_use_ssl">Использовать протокол https</param>
        public SmsContactsGroup(string _project, string _api_key, bool _use_ssl)
        {
            Settings.project = _project;
            Settings.api_key = _api_key;
            Settings.use_ssl = _use_ssl;
        }
        #endregion

        #region Group
        #region List
        /// <summary>
        /// Запрос списка групп
        /// </summary>
        /// <param name="GroupType">Тип запрашиваемых групп</param>
        /// <returns></returns>
        public ResponseGroupList getGroupList(GroupType groupType)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "type", groupType.ToString().ToLower() }
            };
            string response = RequestHelper.post("group_list", queryParams).Result;
            return new ResponseGroupList(response);
        }
        #endregion

        #region Create
        /// <summary>
        /// Создание группы получателей
        /// </summary>
        /// <param name="name">Название группы</param>
        /// <returns></returns>
        public ResponseGroupCreate createGroup(string name)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "name", name }
            };
            string response = RequestHelper.post("group_create", queryParams).Result;
            return new ResponseGroupCreate(response);
        }
        #endregion

        #region Remove
        /// <summary>
        /// Создание группы получателей
        /// </summary>
        /// <param name="id">id группы</param>
        /// <returns></returns>
        public ResponseGroupRemove removeGroup(string id)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "id", id }
            };
            string response = RequestHelper.post("group_remove", queryParams).Result;
            return new ResponseGroupRemove(response);
        }
        #endregion
        #endregion
    }
}
