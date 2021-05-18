using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTO.PostDTOs
{
    public class PostPostDTO
    {
        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string title { get; set; }
        [Required]
        public string content { get; set; }
        [Required]
        public int? categoryID { get; set; }
    }
}
