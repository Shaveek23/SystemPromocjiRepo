using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace IntegrationTest.Extensions
{
    public static class HttpStatusCodeExtension
    {
        public static bool IsOK(this HttpStatusCode statusCode)
        {
            if ((int)statusCode >= 200 && (int)statusCode < 300)
                return true;
            else
                return false;
        }
    }
}
