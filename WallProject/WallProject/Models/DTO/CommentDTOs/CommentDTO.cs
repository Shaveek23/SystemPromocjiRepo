using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WallProject.Models.DTO
{
    public class CommentDTO
    {
        public int id { get; set; }
        public bool ownerMode { get; set; }
        public string content { get; set; }
        public DateTime date { get; set; }
        public int authorID { get; set; }
        public string authorName { get; set; }
        public int likesCount { get; set; }
        public bool isLikedByUser { get; set; }
        public int postId { get; set; }
    }
}
