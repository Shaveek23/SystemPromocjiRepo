using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DTO
{
    public class UserDTO
    {
        [Required]
        public int UserID { get; set; }
        
        [Required]
        [MaxLength(30)]
        [MinLength(1)]
        public string? UserName { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(1)]
        [EmailAddress]
        public string? UserEmail { get; set; }
        public System.DateTime Timestamp { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsEnterprenuer { get; set; }
        public bool IsVerified { get; set; }
        public bool Active { get; set; }


    }
}
