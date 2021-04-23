using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Exceptions;
using WebApi.Models.DTO.PostDTOs;
using WebApi.Models.POCO;
using WebApi.Services;

namespace WebApi.Database.Repositories.Implementations
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DatabaseContext databaseContext) : base(databaseContext) { }

        //TO DO: To chyba powinno być w CommentRepository albo w ogole jako metoda generyczna GetAllOfUser(user id) - wtedy kazdy zasob by musiał być skojarzony z jakimś userId zeby dzialalo dla każdego
        public ServiceResult<IQueryable<Comment>> GetAllComments(int postID)
        {
            var comments = dbContext.Comments.Where(comment => comment.PostID == postID);
            if (comments == null)
            {
                return new ServiceResult<IQueryable<Comment>>((IQueryable<Comment>)(new List<Comment>()));
            }
            return new ServiceResult<IQueryable<Comment>>(comments);
        }

        public ServiceResult<IQueryable<PostLike>> GetLikes(int postID)
        {
            var postLikes = dbContext.PostLikes.Where(like => like.PostID == postID);
            if (postLikes == null)
            {
                return new ServiceResult<IQueryable<PostLike>>((IQueryable< PostLike>)(new List<PostLike>()));
            }
            return new ServiceResult<IQueryable<PostLike>>(postLikes);

        }

        public async Task<ServiceResult<bool>> UpdateLikeStatusAsync(int userID,int postID, bool like)
        {
            if (dbContext.PostLikes.Any(like => like.PostID == postID && like.UserID == userID) && like)
            {
                dbContext.Remove(dbContext.PostLikes.First(like => like.UserID == userID && like.PostID == postID));
                dbContext.SaveChanges();
            }
                

            else
            {
                if (!dbContext.PostLikes.Any(like => like.PostID == postID && like.UserID == userID) && !like)
                {
                    await dbContext.PostLikes.AddAsync(new PostLike { PostID = postID, UserID = userID });
                    dbContext.SaveChanges();
                }
                   
                else
                {
                    return ServiceResult<bool>.GetInternalErrorResult();
                }
            }
            return new ServiceResult<bool>(true);
        }
    }
}
