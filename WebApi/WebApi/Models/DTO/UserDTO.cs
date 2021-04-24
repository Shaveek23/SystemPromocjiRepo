using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DTO
{
    public class UserDTO
    {
        public int? id { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(1)]
        public string? userName { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(1)]
        [EmailAddress]
        public string userEmail { get; set; }

        [Required]
        public System.DateTime? timestamp { get; set; }

        [Required]
        public bool? isAdmin { get; set; }

        [Required]
        public bool? isEnterprenuer { get; set; }

        [Required]
        public bool? isVerified { get; set; }

        [Required]
        public bool? isActive { get; set; }


    }
}
