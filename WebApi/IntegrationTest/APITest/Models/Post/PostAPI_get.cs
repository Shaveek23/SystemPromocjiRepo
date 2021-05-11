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
        public int? category;
        public bool? isPromoted;
        public string author;
        public int authorID;
        public int likesCount;
        public bool isLikedByUser;
        public List<CommentAPI_get> comments;
    }
}
