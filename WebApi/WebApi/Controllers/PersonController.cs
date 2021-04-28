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
    public class PersonController : Controller
    {

        // DEPENDENCY INJECTION: 
        private readonly IPersonService _personService; // this service handles all logic and database updates -> no logic in controllers !!!

        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet("{id}")]
        public ActionResult<int> Get(int id)
        {
            var result = _personService.GetById(id);
            return new ControllerResult<PersonDTO>(result).GetResponse();
        }
        
        [HttpGet]
        public ActionResult<IQueryable<PersonDTO>> GetAll()
        {
            var result = _personService.GetAll();
            return new ControllerResult<IQueryable<PersonDTO>>(result).GetResponse();
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddPerson([FromBody] PersonDTO person)
        { 

            var result = await _personService.AddPersonAsync(person);
            return new ControllerResult<int?>(result).GetResponse();
        }
    }
}
