using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole.Infrastructure
{
    public interface IEndpointConfiguration
    {
        string ConnectionString { get; set; }
        string Subscription { get; set; }
        string Topic { get; set; }
    }
}
