using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.POCO
{
    public class Post : IUserable
    {
        [Key]
        public int PostID { get; set; }
        public int UserID { get; set ; }
        public int CategoryID { get; set; }
        public DateTime Date { get ; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPromoted { get; set; }

        public int GetOwner() => UserID;
    }
}
