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
            SmsContact smsContact = new SmsContact(ConfigurationManager.AppSettings.Get("project"), ConfigurationManager.AppSettings.Get("api_key"), false);

            ResponseContactRemove responseContactRemove = smsContact.removeContact("79609709097, 79609709098");
            if (responseContactRemove.status == "success")
            {
                Console.WriteLine($"Удалено {responseContactRemove.phones.Count} контактов");
            }
            else Console.WriteLine("Error - " + responseContactRemove.message);

        }
    }
}
