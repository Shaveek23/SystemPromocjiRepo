using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.POCO
{
    public class CommentLike : IUserable
    {
        [Key]
        public int CommentLikeID { get; set; }
        public int CommentID { get; set; }
        public int UserID { get; set; }

        public int GetOwner() => UserID;
    }
}
