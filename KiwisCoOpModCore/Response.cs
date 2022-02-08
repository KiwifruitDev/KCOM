using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KiwisCoOpModCore
{
    public class Response
    {
        public string? type;
        public string? data;
        public string? map;
        public string? remoteClientUsername;
        public string? clientUsername;
        public string? clientAuthId;
        public string? password;
        public static int internalVersion = 0;
        public int? version = internalVersion;
        public bool urgent;
        public Response()
        { }
        public Response(string type)
        {
            this.type = type;
        }
        public Response(string type, string data)
        {
            this.type = type;
            this.data = data;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
