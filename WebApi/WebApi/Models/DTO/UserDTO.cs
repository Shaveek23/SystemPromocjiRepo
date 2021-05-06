using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DTO
{
    public class UserDTO
    {
        public int? ID { get; set; }
        
        [Required]
        [MaxLength(30)]
        [MinLength(1)]
        public string? UserName { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(1)]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        public System.DateTime? Timestamp { get; set; }
        
        [Required]
        public bool? IsAdmin { get; set; }
        
        [Required]
        public bool? IsEntrepreneur { get; set; }
        
        [Required]
        public bool? IsVerified { get; set; }
        
        [Required]
        public bool? IsActive { get; set; }


    }
}
