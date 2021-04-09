using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WallProject.Models
{
    public class PostViewModel
    {
        public int? id { get; set; }

        public string title { get; set; }
        public string content { get; set; }
        public DateTime? datetime { get; set; }

        public int? category { get; set; }
        public bool? isPromoted { get; set; }
        public string author { get; set; }
        public int? authorID { get; set; }
        public int? likesCount { get; set; }
        public bool? isLikedByUser { get; set; }
    }
}
