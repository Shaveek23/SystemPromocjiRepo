using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WallProject.Models.DTO
{
    public class CommentGetDTO
    {
        public int ID { get; set; }
        public bool OwnerMode { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public int LikesCount { get; set; }
        public bool IsLikedByUser { get; set; }
        public int PostID { get; set; }
    }
}
