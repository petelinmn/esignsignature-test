using System;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace eDocSignature
{
    partial class Program
    {
        public static string GetHash(string input) =>
            Encoding.ASCII.GetString(SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(input)));

        public static string GetClientIp() =>
            Dns.GetHostEntry(Dns.GetHostName()).AddressList
                .FirstOrDefault(i => i.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.ToString();

        public static string DoRIPActionRequest(string url, string method, object data)
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