using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTO.PostDTOs
{
    //DTO for creating and editing post
    public class PostPutDTO
    {
        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string title { get; set; }
        [Required]
        public string content { get; set; }
        [Required]
        public int? categoryID { get; set; }
        [Required]
        public bool? isPromoted { get; set; }

    }
}
