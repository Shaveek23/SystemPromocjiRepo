using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.POCO
{
    public class Newsletter
    {
        [Key]
        public int NewsletterID { get; set; }

        public int UserID { get; set; }
        
        public int CategoryID { get; set; }

    }
}
