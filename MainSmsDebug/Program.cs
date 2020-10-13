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
            SmsSending smsSending = new SmsSending(ConfigurationManager.AppSettings.Get("project"), ConfigurationManager.AppSettings.Get("api_key"), true);
            SendingInfo sendingInfo = new SendingInfo();
            sendingInfo.include = "141606";
            sendingInfo.exclude = "141607";
            sendingInfo.sender = "mainsms";
            sendingInfo.message = "test_api_sending";
            ResponseSendingCreate responseSendingCreate = smsSending.createSending(sendingInfo);
            if (responseSendingCreate.status == "success") Console.WriteLine($"Рассылка {responseSendingCreate.id} создана");
            else Console.WriteLine("Error - " + responseSendingCreate.message);

            ResponseSendingStatus responseSendingStatus = smsSending.sendingStatus("166937");
            if (responseSendingStatus.status == "success")
            {
                Console.WriteLine($"Отправленно {responseSendingStatus.total}, доставлено {responseSendingStatus.delivered}");
            }
            else Console.WriteLine("Error - " + responseSendingStatus.message);
        }
    }
}
