using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTO.UserDTOs
{
    public class UserGetDTO
    {
        [Required]
        public int id{ get; set; }
    [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string userName { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string userEmail { get; set; }
        [Required]
        public bool isAdmin { get; set; }
        [Required]
        public bool isEntrepreneur { get; set; }
        [Required]
        public bool isVerified { get; set; }
        [Required]
        public bool isActive { get; set; }
    }
}
