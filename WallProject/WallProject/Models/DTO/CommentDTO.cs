using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WallProject.Models.DTO
{
    public class CommentDTO
    {
        public int commentID;
        public int userID;
        public int postID;
        public DateTime dateTime;
        public string content;
    }
}
