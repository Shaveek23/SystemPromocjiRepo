using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WallProject.Models
{
    public class PostViewModel
    {
        public string Title;
        public string Content;
        public DateTime Datetime;
        public int Category;
        public bool IsPromoted;
        public PersonViewModel Owner;
        public string OwnerName; //TO DO: po implementacji User usunąć;
        public int Likes;
        public bool IsLikedByUser;
        public List<CommentViewModel> Comments;
    }
}
