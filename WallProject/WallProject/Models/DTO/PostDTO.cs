using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WallProject.Models.DTO
{
    public class PostDTO
    {
        public int id;
        public string title;
        public string content;
        public DateTime datetime;
        public int category;
        public bool isPromoted;
        public string author; //not used
        public int authorID;
        public int likesCount;
        public bool IsLikedByUser;
    }
}
