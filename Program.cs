using System;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace eDocSignature
{
    class Program
    {
        static void Main(string[] args)
        {       
            string RIPADDRESS = "http://edocsign.com";
            string ControlId = "Reed";
            var userhost = GetClientIp();
            var data = new {
                user = "user",
                passwordhash = "",
                host = userhost,
                controlid = ControlId
            };

            var result = DoRIPActionRequest(RIPADDRESS + "/SESSIONS", "POST", data);
            Console.WriteLine(result);
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
