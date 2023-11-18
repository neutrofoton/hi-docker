using System.Runtime.Serialization;

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
