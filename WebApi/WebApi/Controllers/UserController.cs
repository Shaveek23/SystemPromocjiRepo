using System;
using System.Collections.Generic;
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
    /* when using [ApiController] validation is performed autmatically - don't need to execute: 
        if (!ModelState.IsValid)           // Invokes the build-in
            return BadRequest(ModelState); // validation mechanism
        
        in case of error 400 status code is returned with ModelState as shown above.
        Validation rules are defined by dto class attributes (i.e: [Required]) or Validate method
    */

    [Route("api/[controller]")]
    public class UserController : Controller
    {

        // DEPENDENCY INJECTION: 
        private readonly IUserService _userService; // this service handles all logic and database updates -> no logic in controllers !!!

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public ActionResult<int> Get(int id)
        {
            var result = _userService.GetById(id);
            return new ControllerResult<UserDTO>(result).GetResponse();
        }

        [HttpGet]
        public ActionResult<IQueryable<UserDTO>> GetAll()
        {
            var result = _userService.GetAll();
            return new ControllerResult<IQueryable<UserDTO>>(result).GetResponse();
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddUser([FromBody] UserDTO user)
        {

            var result = await _userService.AddUserAsync(user);
            return new ControllerResult<int?>(result).GetResponse();
        }
    }
}
