using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Controllers
{
    public class ControllerResult<T>
    {
        private readonly ServiceResult<T> _serviceResult;
        public ControllerResult(ServiceResult<T> serviceResult)
        {
            this._serviceResult = serviceResult;
        }

        public ActionResult GetResponse() // ActionResult is an abstract class and have many derived types, including ObjectResult (converted to Json or plain text depending on which type has been requested in accept headers) and JsonResult (always as json)
        {
            ObjectResult res;
            if (!_serviceResult.IsOk())
            {
               res = new ObjectResult(new { message = _serviceResult.Message});
            }
            else
            {
                res = new ObjectResult(_serviceResult.Result);
            }

            res.StatusCode = (int)_serviceResult.Code;
            
            return res;
        }
        
    }
}
