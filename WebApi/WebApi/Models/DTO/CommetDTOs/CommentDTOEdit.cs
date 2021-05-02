using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTO
{
    public class CommentDTOEdit
    {
        [Required]
        [MaxLength(255)]
        [MinLength(1)]
        public string Content { get; set; }
    }
}
