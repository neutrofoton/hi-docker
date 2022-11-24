using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyRedis.Caching
{
    public class CacheException : ApplicationException
    {

        public CacheException() : base() { }
        public CacheException(string? message) : base(message) { }
        public CacheException(string? message, Exception? innerException) : base(message, innerException) { }
        protected CacheException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
