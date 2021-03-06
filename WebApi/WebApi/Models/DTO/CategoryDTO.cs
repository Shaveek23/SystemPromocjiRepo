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
        public int? ID { get; set; }
        
        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Name { get; set; }
    }
}
