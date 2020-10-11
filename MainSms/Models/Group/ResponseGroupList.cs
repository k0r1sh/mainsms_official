using MainSms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MainSms
{
    public class ResponseGroupList : Response
    {
        /// <summary>
        /// Ответ на запрос списка групп
        /// </summary>
        public ResponseGroupList(string data) : base(data) { }
        
        private List<RecipientsGroup> _recipientsGroups = new List<RecipientsGroup>();
        /// <summary>
        /// Список ответов по запрашиваемым номерам
        /// </summary>
        public List<RecipientsGroup> recipientsGroups
        {
            get { return _recipientsGroups; }
        }

        protected override void storeArray(XElement arrayElement)
        {
            switch (arrayElement.Name.ToString())
            {
                case "result":
                    RecipientsGroup resipientsGroup = new RecipientsGroup();
                    foreach (var element in arrayElement.Elements())
                    {
                            switch (element.Name.ToString())
                            {
                                case "id":
                                    resipientsGroup.id = element.Value;
                                    break;
                                case "contacts":
                                    resipientsGroup.contacts = Convert.ToInt32(element.Value);
                                    break;
                                case "name":
                                    resipientsGroup.name = element.Value;
                                    break;
                                case "type":
                                    resipientsGroup.type = (GroupType) Enum.Parse(typeof(GroupType), element.Value, true);
                                    break;
                            }
                    }
                    _recipientsGroups.Add(resipientsGroup);
                    break;
            }
            if (!variables.ContainsKey("status")) variables["status"] = "success";
        }
    }

    /// <summary>
    /// Информация группе получателей
    /// </summary>
    public struct RecipientsGroup
    {
        /// <summary>
        /// Уникальный идентификатор группы
        /// </summary>
        public string id;
        /// <summary>
        /// Количество контактов в группе
        /// </summary>
        public int contacts;
        /// <summary>
        /// Тип группы
        /// </summary>
        public GroupType type;
        /// <summary>
        /// Название группы получателей
        /// </summary>
        public string name;
    }
}