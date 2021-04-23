using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.POCO
{
    public class PostLike
    {
        [Key]
        public int PostLikeID { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
    }
}