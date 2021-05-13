using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTO
{
    public class idDTO
    {
        [Required]
        public int? id { get; set; }
    }
}
