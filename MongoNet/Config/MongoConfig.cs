using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoNet.Config
{
    public class MongoConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AuthenticationDatabase { get; set; }
        public string DatabaseName { get; set; }
    }
}
