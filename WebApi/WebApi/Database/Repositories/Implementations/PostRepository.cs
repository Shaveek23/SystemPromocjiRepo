using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Exceptions;
using WebApi.Models.DTO.PostDTOs;
using WebApi.Models.POCO;

namespace WebApi.Database.Repositories.Implementations
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DatabaseContext databaseContext) : base(databaseContext) { }

        // To chyba powinno być w CommentRepository albo w ogole jako metoda generyczna GetAllOfUser(user id) - wtedy kazdy zasob by musiał być skojarzony z jakimś userId zeby dzialalo dla każdego
        public IQueryable<Comment> GetAllComments(int postID)
        {
            var comments = dbContext.Comments.Where(comment => comment.PostID == postID);
            if (comments==null)
            {
                return (IQueryable<Comment>)(new List<Comment>());
            }
            return comments;
        }

    }
}
