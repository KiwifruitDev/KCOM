using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwisCoOpModCore
{
    public class Client
    {
        public string Username { get; set; } = "";
        public string AuthId { get; set; } = "";
        public Client(string username, string authid)
        {
            Username = username;
            AuthId = authid;
        }
    }
    
}
