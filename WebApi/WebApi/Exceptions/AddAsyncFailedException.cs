using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Exceptions
{
    public class AddAsyncFailedException : Exception
    {
        public AddAsyncFailedException(string message = null, Exception exception = null) : base(message, exception) { }
    }

}
