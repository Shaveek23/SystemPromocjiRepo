using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WallProject.Models
{
    public class PostViewModel
    {
        public int Likes;
        public PersonViewModel Owner;
        public DateTime Time;
        public string Content;
        public List<CommentViewModel> Comments;
        public bool IsPromoted;
        public string Localization;
    }
}
