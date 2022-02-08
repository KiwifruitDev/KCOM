using KiwisCoOpModCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleck;

namespace KiwisCoOpModCore
{
    public class IndexedClient : Client
    {
        public string Map = "";
        public IWebSocketConnection Session;
        public IndexedClient(IWebSocketConnection session, string username, string authId, string map) : base(username, authId)
        {
            Map = map;
            Session = session;
        }
    }
}
