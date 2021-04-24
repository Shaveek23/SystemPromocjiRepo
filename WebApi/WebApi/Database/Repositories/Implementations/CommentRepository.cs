using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Exceptions;
using WebApi.Models.POCO;
using WebApi.Services;

namespace WebApi.Database.Repositories.Implementations
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(DatabaseContext databaseContext) : base(databaseContext) { }


        public ServiceResult<IQueryable<CommentLike>> GetLikes(int commentID)
        {
            var commentLikes = dbContext.CommentLikes.Where(like => like.CommentID == commentID);
            if (commentLikes == null)
            {
                return new ServiceResult<IQueryable<CommentLike>>((IQueryable<CommentLike>)(new List<CommentLike>()));
            }
            return new ServiceResult<IQueryable<CommentLike>>(commentLikes);
        }

        public async Task<ServiceResult<bool>> UpdateLikeStatusAsync( int commentID, int userID, bool like)
        {
            try
            {
                if (dbContext.CommentLikes.Any(like => like.CommentID== commentID && like.UserID == userID))
                {
                    dbContext.Remove(dbContext.CommentLikes.First(like => like.UserID == userID && like.CommentID == commentID));
                    dbContext.SaveChanges();
                }

                else
                {

                    await dbContext.CommentLikes.AddAsync(new CommentLike { CommentID = commentID, UserID = userID });
                    dbContext.SaveChanges();

                }
            }
            catch (Exception e)
            {
                return ServiceResult<bool>.GetInternalErrorResult();
            }


            return new ServiceResult<bool>(true);
        }
    }
}
