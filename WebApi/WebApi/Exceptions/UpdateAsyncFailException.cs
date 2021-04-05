using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Exceptions
{
    public class UpdateAsyncFailException : Exception
    {
        public UpdateAsyncFailException(string message = null, Exception exception = null) : base(message, exception) { }
    }

}
