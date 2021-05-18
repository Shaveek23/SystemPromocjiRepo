using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WallProject.Models.DTO
{
    public class PostGetDTO
    {
        public int ID;
        public string Title;
        public string Content;
        public DateTime Datetime;
        public string Category;
        public bool IsPromoted;
        public string AuthorName; 
        public int AuthorID;
        public int LikesCount;
        public bool IsLikedByUser;
    }
}
