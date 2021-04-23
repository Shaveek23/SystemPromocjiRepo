using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.POCO;
using WebApi.Services;

namespace WebApi.Database.Repositories.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        ServiceResult<IQueryable<CommentLike>> GetLikes(int commentId);
        Task<ServiceResult<bool>> UpdateLikeStatusAsync( int commentID, int userID, bool like);


    }
}
