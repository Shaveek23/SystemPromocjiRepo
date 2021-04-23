using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTO
{
    public class PostLikeDTO
    {
        [Required]
        public int PostLikeID { get; set; }
        [Required]
        public int UserID { get; set; }

        [Required]
        public int PostID { get; set; }
    }
}
