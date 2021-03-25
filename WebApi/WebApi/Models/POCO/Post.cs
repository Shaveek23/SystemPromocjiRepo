using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.POCO
{
    public class Post : IPostable
    {
        public int UserId { get; set ; }
        public DateTime Date { get ; set; }
        public List<int> Likes { get; set; }
        public IPostableData Content { get; set; }
    }
}
