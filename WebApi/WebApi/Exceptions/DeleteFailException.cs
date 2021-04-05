using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Exceptions
{
    public class DeleteFailException : Exception
    {
        public DeleteFailException(string message = null, Exception exception = null) : base(message, exception) { }
    }
}
