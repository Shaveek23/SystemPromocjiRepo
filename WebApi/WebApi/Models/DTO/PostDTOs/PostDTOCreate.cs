using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTO.PostDTOs
{
    public class PostDTOCreate
    {
        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string title;
        [Required]
        public string content;
        [Required]
        public int? category;
    }
}
