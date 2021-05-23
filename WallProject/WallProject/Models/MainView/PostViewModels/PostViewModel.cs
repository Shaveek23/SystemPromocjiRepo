using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models.MainView;

namespace WallProject.Models
{
    public class PostViewModel
    {
        public int Id;
        public string Title;
        public string Content;
        public DateTime Datetime;
        public string Category;
        public bool IsPromoted;
        public UserViewModel CurrentUser;
        public UserViewModel Owner;
        public string OwnerName; //TO DO: po implementacji User usunąć;
        public int Likes;
        public bool IsLikedByUser;
        public List<CommentViewModel> Comments;
    }
}
