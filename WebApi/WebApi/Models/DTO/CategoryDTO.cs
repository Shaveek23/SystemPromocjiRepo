using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTO
{
    public class CategoryDTO
    {
        [Key]
        [Required]
        public int? id { get; set; }
        
        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string name { get; set; }
    }
}
