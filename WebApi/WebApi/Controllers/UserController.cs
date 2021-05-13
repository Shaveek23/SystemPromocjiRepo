using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Models.DTO;
using WebApi.Services.Services_Implementations;
using WebApi.Services.Services_Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {

        // DEPENDENCY INJECTION: 
        private readonly IUserService _userService;

        private readonly ILogger<UserController> _logger;

        private readonly INewsletterService _newsletterService;

        public UserController(ILogger<UserController> logger, IUserService userService, INewsletterService newsletterService)
        {
            _logger = logger;
            _userService = userService;
            _newsletterService = newsletterService;
        }

        [HttpGet("users")]
        public ActionResult<IQueryable<UserDTO>> GetAll()
        {
            var result = _userService.GetAll();
            return new ControllerResult<IQueryable<UserDTO>>(result).GetResponse();
        }

        [HttpGet("users/{UserID}")]
        public ActionResult<UserDTO> Get([Required][FromRoute] int  UserID)
        {
            var result = _userService.GetById(UserID);
            return new ControllerResult<UserDTO>(result).GetResponse();
        }


        [HttpPost("users")]
        public async Task<ActionResult<int>> AddUser([Required][FromHeader] int UserID, [FromBody] UserDTO user)
        {

            var result = await _userService.AddUserAsync(UserID,user);
            return new ControllerResult<int?>(result).GetResponse();
        }

        [HttpPut("users/{UserToBeEdited}")]
        public async  Task<ActionResult<bool>> EditUser([Required][FromHeader] int UserID, [FromBody] UserDTO userDTO, [FromRoute] int UserToBeEdited)
        {
            var result = await _userService.EditUserAsync(UserID, userDTO, UserToBeEdited);
            return new ControllerResult<bool>(result).GetResponse();
        }

        [HttpDelete("users/{UserToBeDeletedId}")]
        public async Task<ActionResult<bool>> DeleteUser([Required][FromHeader] int UserID, [FromRoute] int UserToBeDeletedId)
        {
            var result = await _userService.DeleteUserAsync(UserID, UserToBeDeletedId);
            return new ControllerResult<bool>(result).GetResponse();

        }

        [HttpGet("users/{id}/subscribedCategories")]
        public ActionResult<IQueryable<idDTO>> getSubscribedCategories([Required][FromHeader] int UserID, [FromRoute] int id)
        {
            var result = _newsletterService.getSubscribedCategories(id);
            return new ControllerResult<IQueryable<idDTO>>(result).GetResponse();
        }
       
    }
}
