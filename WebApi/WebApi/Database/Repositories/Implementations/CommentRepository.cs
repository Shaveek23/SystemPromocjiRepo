using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Exceptions;
using WebApi.Models.POCO;

namespace WebApi.Database.Repositories.Implementations
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(DatabaseContext databaseContext) : base(databaseContext) { }

        #region TODO: Przeniesc do generycznego
        public bool DeleteComment(int id, int userId)
        {
            try
            {
                dbContext.Remove(dbContext.Find<Comment>(id));
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                throw new DeleteFailException("Fail when trying to delete the comment");
            }
           
        }
        #endregion

        public Task EditLikeOnComment(int commentId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<int>> GetLikedUsersAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
