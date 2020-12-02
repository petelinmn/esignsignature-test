using System;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace eDocSignature
{
    class Program
    {
        static void Main(string[] args)
        {
            var RIPADDRESS = "http://edocsign.com";
            var ControlId = "Reed";
            var password = "ASDAT454fФЫВ";
            var sha1 = SHA1.Create();
            var passwordhash = Encoding.ASCII.GetString(sha1.ComputeHash(Encoding.ASCII.GetBytes(password)));
            var userhost = GetClientIp();
            var data = new {
                user = "user",
                passwordhash = passwordhash,
                host = userhost,
                controlid = ControlId
            };

            //var result = DoRIPActionRequest(RIPADDRESS + "/SESSIONS", "POST", data);
            //Console.WriteLine(result);
        }

        static string GetClientIp() =>
            Dns.GetHostEntry(Dns.GetHostName()).AddressList
                .FirstOrDefault(i => i.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.ToString();

        static string DoRIPActionRequest(string url, string method, object data)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = method;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(data);
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
