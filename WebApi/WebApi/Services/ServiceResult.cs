using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public class ServiceResult<T>
    {
        public ServiceResult(T result, HttpStatusCode code = HttpStatusCode.OK, string message = null, string name = null)
        {
            Result = result;
            Code = code;
            Message = message;
            Name = null;
        }

        public static ServiceResult<T> GetEntityNullResult()
        {
            return new ServiceResult<T>(default(T), HttpStatusCode.BadRequest, "Entity cannot be null.");
        }

        public static ServiceResult<T> GetInternalErrorResult()
        {
            return new ServiceResult<T>(default(T), HttpStatusCode.InternalServerError, "Internal server error occured.");
        }

        public static ServiceResult<T> GetResourceNotFoundResult()
        {
            return new ServiceResult<T>(default(T), HttpStatusCode.BadRequest, "Requested resource has not been found.");
        }

        public static ServiceResult<T> GetUserNotAuthorized()
        {
            return new ServiceResult<T>(default(T), HttpStatusCode.BadRequest, "User not authorized.");
        }

        public T Result { get; set; }

        public HttpStatusCode Code { get; set; }

        public string Message { get; set; }

        public string Name { get; set; } = null;

        public bool IsOk()
        {
            return ((int)Code >= 200) && ((int)Code <= 299);
        }
    }

}
