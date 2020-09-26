using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace MainSms.Libs
{
    public static class RequestHelper
    {
        private static string url(string url_key)
        {
            string schema = Settings.use_ssl ? "https://" : "http://";
            return schema + Settings.host + Settings.apiPaths[url_key];
        }

        public static async Task<string> post(string url_key, Dictionary<string, string> _postParams = null)
        {
            HttpClient httpClient = new HttpClient();
            Dictionary<string, string> postParams = new Dictionary<string, string>()
            {
                { "project", Settings.project },
                { "format", "xml" }
            };

            if (null != _postParams)
            {
                foreach(KeyValuePair<string, string> pair in _postParams)
                {
                    postParams.Add(pair.Key, pair.Value);
                }
            }
            postParams.Add("sign", generateSign(postParams));

            string result;
            var queryString = new System.Net.Http.FormUrlEncodedContent(postParams);

                using (var postResult = await httpClient.PostAsync(url(url_key), queryString).ConfigureAwait(continueOnCapturedContext: false))
                {
                    result = await postResult.Content.ReadAsStringAsync();
                }

            return result;
        }

        private static string generateSign(Dictionary<string, string> queryParams)
        {
            var valuesArray = (from p in queryParams
                                          orderby p.Key
                                          select (string)p.Value).ToArray();

            var valuesString = string.Join(";", valuesArray);

            string sign = heshing(MD5.Create(), heshing(SHA1.Create(), $"{valuesString};{Settings.api_key}"));

            return sign;
        }

        private static string heshing(HashAlgorithm hashString, string text)
        {
            byte[] hashData;
            byte[] message = Encoding.UTF8.GetBytes(text);
            string result = "";
            hashData = hashString.ComputeHash(message);
            foreach (byte x in hashData)
            {
                result += String.Format("{0:x2}", x);
            }
            return result;
        }
    }
}
