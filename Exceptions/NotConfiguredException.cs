using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicList2.Exceptions
{
    public class NotConfiguredException : Exception
    {
        public NotConfiguredException() { }

        public NotConfiguredException(string key) 
            : base($"Key '{key}' was not configured") { }

        public NotConfiguredException(string message, Exception inner)
            : base(message, inner) {}
    }
}
