using MainSms;
using System;
using System.Configuration;
using System.Collections.Specialized;

namespace MainSmsDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            SmsMessage smsMessage = new SmsMessage(ConfigurationManager.AppSettings.Get("project"), ConfigurationManager.AppSettings.Get("api_key"), false);
            //ResponseBalance response = mainSms.getBalance();

            //ResponsePrice response = mainSms.getMessagesPrice("mainsms", "79609708097, 79545551232", "ruasdasdasdasdaвфывфывфывфывфывфывфывфывфывфывфывфывn at 23:00 olo+ + lo");
            // response.price

            // "236387643", "236395928"
            // ResponseSend response = mainSms.sendSms("mainsms", "79609708091", "test2 message +", DateTime.Parse("30.09.2020 23:00"));

            // ResponseStatus response = mainSms.getMessagesStatus("236422916, 236423069");

            //ResponseCancel response = mainSms.cancelMessages("236428102");

            //ResponseInfo response = mainSms.getPhonesInfo("9138857567");
            //response.info[0]

            // viber 236422916, 236423069
            //ResponseSend response = mainSms.sendViberOrSms("test sender", "79609708091", "viber or sms text3", "Тестовое вайбер или смс сообщение3", "https://mainsms.ru/assets/mainsms-9d32d5b8cc10940ef7b0b1791adbce57.png", "Тест MainSms", "https://mainsms.ru");

            SmsRecipientsGroup smsRecipientsGroup = new SmsRecipientsGroup(ConfigurationManager.AppSettings.Get("project"), ConfigurationManager.AppSettings.Get("api_key"), false);

             ResponseGroupList responseGroupList = smsRecipientsGroup.getGroupList(GroupType.User);

             ResponseGroupCreate responseGroupCreate = smsRecipientsGroup.createGroup("api test group2");

            ResponseGroupRemove responseGroupRemove = smsRecipientsGroup.removeGroup("141440");
            // id = "141360"
        }
    }
}
