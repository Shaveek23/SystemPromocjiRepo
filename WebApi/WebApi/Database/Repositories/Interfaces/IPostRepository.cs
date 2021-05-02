using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.POCO;
using WebApi.Services;

namespace WebApi.Database.Repositories.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {

        ServiceResult<IQueryable<Comment>> GetAllComments(int postID);
        ServiceResult<IQueryable<PostLike>> GetLikes(int postID);
        Task<ServiceResult<bool>> UpdateLikeStatusAsync(int userID,int postID, bool like);
    }
}
