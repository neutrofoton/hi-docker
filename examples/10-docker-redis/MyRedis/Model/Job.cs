using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyRedis.Caching.Model;

namespace MyRedis.Model
{
    public class Job : CachedModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime At { get; set; }
    }
}
