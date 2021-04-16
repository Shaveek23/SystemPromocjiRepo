using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.POCO
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        public string Name { get; set; }
    }
}
