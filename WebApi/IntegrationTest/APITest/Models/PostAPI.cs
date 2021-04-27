using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationTest
{
    public class PostAPI
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public int? Category { get; set; }
        public DateTime? Datetime { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool? IsPromoted { get; set; }
    }
}
