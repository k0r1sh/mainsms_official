using System;
using System.Collections.Generic;
using System.Text;

namespace MainSms
{
    public static class Settings
    {
        public static bool use_ssl = false;
        public static bool is_test = false;
        public static string host = "mainsms.ru/api/mainsms";
        public static string project = "";
        public static string api_key = "";

        public static Dictionary<string, string> apiPaths = new Dictionary<string, string>()
        {
            {"message_send", "/message/send"},
            {"message_status", "/message/status"},
            {"message_cancel", "/message/cancel"},
            {"message_price", "/message/price"},
            {"message_balance", "/message/balance"},
            {"message_info", "/message/info"},

            { "group_list", "/group/list" },
            { "group_remove", "/group/remove" },
            { "group_create", "/group/create" },

            { "contact_create", "/contact/create" },
            { "contact_remove", "/contact/remove" },
            { "contact_exists", "/contact/exists" }
        };
    }
}
