using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WallProject.Models
{
    public class PostViewModel
    {
        public int Likes { get; set; }
        public PersonViewModel Owner { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public bool IsPromoted { get; set; }
        public string Localization { get; set; }
    }
}
