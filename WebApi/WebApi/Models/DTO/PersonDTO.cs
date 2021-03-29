using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTO
{

    // Validation: a sole using of validation attributes (like [Required]) or implementing IValidatableObject does not mean
    // that a dto object will be validated before executing a controller action (adding a custom filter validating request is needed) 
    // but for controllers with attribute [ApiController] (Web Api controllers) that validation is performed automatically (no need to add a custom filter),
    // in case of error an automatic HTTP 400 response containing error details is returned (it is possible to supress that with defining a custom filter and changing configuration in startup.cs)

    public class PersonDTO : IValidatableObject // interface with Validate method
    {
        [Required]  // more information about attributes: https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-5.0#validation-attributes
        public int PersonID { get; set; } 

        [Required]
        [MaxLength(30)] // in accordance to the limits in databaseContext
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(30)]
        public string Address { get; set; }

        [Required]
        [MaxLength(30)]
        public string City { get; set; }

        
        // An additional validating method can be added for more complex validation (i.e.: FirstName or LastName required, but not both at the same time), 
        // which may be impossible to be reached with default validation attributes
        // it also gives you a context with the posibility to not only validate some plain input texts,
        // you also can access o your dependency injected services and validate against logic or your database.

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FirstName == "" || City == null)
                yield return new ValidationResult("Person must have a name and live somewhere", new[] { nameof(PersonDTO) });

        }

    }
}
