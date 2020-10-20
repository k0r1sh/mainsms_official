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
            SmsMessage sms = new SmsMessage(ConfigurationManager.AppSettings.Get("project"), ConfigurationManager.AppSettings.Get("api_key"), true);
            ResponseInfo responseInfo = sms.getPhonesInfo("9138857567");
            if (responseInfo.status == "success") Console.WriteLine($"Номер 9138857567 принадлежит оператору - {responseInfo.info[0].name}");
            else Console.WriteLine("Error - " + responseInfo.message);
        }
    }
}
