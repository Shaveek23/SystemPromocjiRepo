using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DTO
{
    public class UserDTO: IValidatableObject
    {
        [Required]
        public int UserID { get; set; }
        
        [Required]
        [MaxLength(30)]
        [MinLength(1)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(1)]
        [EmailAddress]
        public string UserEmail { get; set; }
        public System.DateTime Timestamp { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsEnterprenuer { get; set; }
        public bool IsVerified { get; set; }
        public bool Active { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UserName == "" || UserEmail == null)
                yield return new ValidationResult("User must have a name and an email", new[] { nameof(PersonDTO) });
        }
    }
}
