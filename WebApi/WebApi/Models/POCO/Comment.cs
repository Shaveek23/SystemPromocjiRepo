using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApi.Models.POCO
{
    public class Comment : IUserable
    {
        [Key]
        public int CommentID { get; set; }
        public int UserID { get; set; }
        public int PostID { get; set; }
        public DateTime DateTime { get; set; }
        public string Content { get; set; }

        public int GetOwner() => UserID;
    }
}
