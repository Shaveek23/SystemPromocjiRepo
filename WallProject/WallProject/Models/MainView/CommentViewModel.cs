using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WallProject.Models
{
    public class CommentViewModel
    {
        public int Likes;
        public PersonViewModel Owner;
        public string OwnerName; //do wyświetlenia userID w ramach prezentacji
        public DateTime Time;
        public string Content;
    }
}
