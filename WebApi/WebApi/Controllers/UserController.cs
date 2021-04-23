using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Models.DTO;
using WebApi.Services.Services_Interfaces;

namespace WebApi.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class UserController : Controller
    {

        // DEPENDENCY INJECTION: 
        private readonly IUserService _userService; 

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("{UserID}")]
        public ActionResult<UserDTO> Get([Required][FromRoute] int  UserID)
        {
            var result = _userService.GetById(UserID);
            return new ControllerResult<UserDTO>(result).GetResponse();
        }

        [HttpGet]
        public ActionResult<IQueryable<UserDTO>> GetAll()
        {
            var result = _userService.GetAll();
            return new ControllerResult<IQueryable<UserDTO>>(result).GetResponse();
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddUser([Required][FromHeader] int UserID, [FromBody] UserDTO user)
        {

            var result = await _userService.AddUserAsync(UserID,user);
            return new ControllerResult<int?>(result).GetResponse();
        }

        [HttpPut("{UserToBeEdited}")]
        public async  Task<ActionResult<bool>> EditUser([Required][FromHeader] int UserID, [FromBody] UserDTO userDTO, [FromRoute] int UserToBeEdited)
        {
            var result = await _userService.EditUserAsync(UserID, userDTO, UserToBeEdited);
            return new ControllerResult<bool>(result).GetResponse();
        }

        [HttpDelete("{UserToBeDeletedId}")]
        public async Task<ActionResult<bool>> DeleteUser([Required][FromHeader] int UserID, [FromRoute] int UserToBeDeletedId)
        {
            var result = await _userService.DeleteUserAsync(UserID, UserToBeDeletedId);
            return new ControllerResult<bool>(result).GetResponse();

        }
    }
}
