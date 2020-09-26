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
            Sms mainSms = new Sms(ConfigurationManager.AppSettings.Get("project"), ConfigurationManager.AppSettings.Get("api_key"), false);
            //ResponseBalance response = mainSms.getBalance();

            //ResponsePrice response = mainSms.getMessagesPrice("mainsms", "79609708097, 79545551232", "run at 23:00 olo+ + lo");

            // "236387643", "236395928"
             // ResponseSend response = mainSms.sendSms("mainsms", "79609708091", "test2 message +", DateTime.Parse("30.09.2020 23:00"));

            // ResponseStatus response = mainSms.getMessagesStatus("236422916, 236423069");

            // ResponseCancel response = mainSms.cancelMessages("236428102");

            ResponseInfo response = mainSms.getPhonesInfo("9138857567");

            // viber 236422916, 236423069
            //ResponseSend response = mainSms.sendViberOrSms("test sender", "79609708091", "viber or sms text3", "Тестовое вайбер или смс сообщение3", "https://mainsms.ru/assets/mainsms-9d32d5b8cc10940ef7b0b1791adbce57.png", "Тест MainSms", "https://mainsms.ru");

        }
    }
}
