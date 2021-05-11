using System;

namespace IntegrationTest.APITest.Models.Comment
{
    public class CommentAPI_get
    {
        public int id;
        public bool ownerMode;
        public string content;
        public DateTime date;
        public int authorID;
        public string authorName;//TO DO: zmienić na author z name i surname
        public int likesCount;
        public bool isLikedByUser;
        public int PostId;
    }
}
