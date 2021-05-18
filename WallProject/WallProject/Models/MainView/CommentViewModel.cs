using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models.MainView;

namespace WallProject.Models
{
    public class CommentViewModel
    {
        public int Id;

        public UserViewModel Owner;
        public string OwnerName; //do wyświetlenia userID w ramach prezentacji
        public bool OwnerMode;

        public int Likes;
        public bool IsLikedByUser;

        public DateTime Time;
        public string Content;

        public int PostID;
    }
}
