using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchPay.Model;

namespace BatchPay.Service
{
    public class LoginService
    {
        public static void TryLogin(string _username, string _password, out string message)
        {
            try
            {
                message = "";

                using (var batchDB = new BatchPayEntities1())
                {
                    var user = batchDB.UserAccount.FirstOrDefault(r => r.Username == _username && r.Password == _password);

                    if(user == null)
                    {
                        message = "Invalid username or password";
                    }
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }
    }
}
