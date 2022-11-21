using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRedis.Model
{
    public class Job : IModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime At { get; set; }
    }
}
