using IntegrationTest.APITest.Models.Comment;
using System;
using System.Collections.Generic;

namespace IntegrationTest.APITest.Models.Post
{
    public class PostAPI_get
    {
        public int id;
        public string title;
        public string content;
        public DateTime? datetime;
        public string category;
        public bool? isPromoted;
        public string authorName;
        public int authorID;
        public int likesCount;
        public bool isLikedByUser;
    }
}
