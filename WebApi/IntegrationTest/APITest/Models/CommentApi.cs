using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationTest.APITest.Models
{
    public class CommentApi
    {
        public int CommentID { get; set; }
        public int UserID { get; set; }
        public int PostID { get; set; }
        public DateTime DateTime { get; set; }
        public string Content { get; set; }
    }
}
