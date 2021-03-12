using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTO
{
    public class PersonDTO
    {
        public int PersonID { get; set; } // PersonID can be deleted to prevent sensitive or internal data from manipulation

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        // Additional validating method CAN be added to provide data consistency 
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FirstName == "" || City == null)
                yield return new ValidationResult("Person must have a name and live somewhere", new[] { nameof(PersonDTO) });

        }

    }
}
