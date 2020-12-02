using System;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace eDocSignature
{
    public class Config
    {
        public static string RipAddress { get; } = "https://test";
        public static string ControlId { get; } = "testConrolId";
        public static string User { get; } = "testUser";
        public static string Password { get; } = "testPassword";
    }
}