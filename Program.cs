

namespace eDocSignature
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var logdata = Login();
        }


        static string Login() =>
            DoRIPActionRequest(Config.RipAddress + "/SESSIONS", "POST", new {
                user = Config.User,
                passwordhash = GetHash(Config.Password),
                host = GetClientIp(),
                controlid = Config.ControlId
            });

        static string Logout(string Session) =>
            DoRIPActionRequest(Config.RipAddress + "/SESSIONS", "DELETE", new {
                session = Session,
                host = GetClientIp(),
                controlid = Config.ControlId
            });
    }
}
