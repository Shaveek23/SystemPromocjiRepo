using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTO
{
    public class CommentLikeDTO
    {
        [Required]
        public int CommentLikeID { get; set; }
        [Required]
        public int UserID { get; set; }

        [Required]
        public int CommentID { get; set; }
    }
}
