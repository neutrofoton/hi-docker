using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRedis.Caching.Model
{
    public class RedisCacheConfiguration
    {
        public string Name { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }

        public string ServerFullName
        {
            get
            {
                return $"{Server}:{Port}";
            }
        }
    }
}
