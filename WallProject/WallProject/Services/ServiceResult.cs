using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WallProject.Models;

namespace WallProject.Services
{
    public class ServiceResult<T>
    {
        public T Result { get; }
        public HttpStatusCode Code { get; }
        public string Message { get; }

        public ServiceResult(T result, HttpStatusCode code = HttpStatusCode.OK, string message = null)
        {
            Result = result;
            Code = code;
            Message = message;
        }

        public ServiceResult(string jsonString, HttpStatusCode code)
        {
            Code = code;

            if ((int)code >=200 && (int)code<=299)
                Result = JsonConvert.DeserializeObject<T>(jsonString);
            else
                Message = jsonString; //TO DO: zdeserializować
        }

        public bool IsOk()
        {
            return ((int)Code >= 200) && ((int)Code <= 299);
        }

        public static ServiceResult<T> GetMessage(string jsonString, HttpStatusCode code)
        {
            return new ServiceResult<T>(default, HttpStatusCode.BadRequest,jsonString ); //TO DO: zdeserializować
        }

    }
}
