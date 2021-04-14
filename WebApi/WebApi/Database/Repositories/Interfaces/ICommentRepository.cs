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

        //TODO:Zamienic na Users
        Task<ServiceResult<IQueryable<int>>> GetLikedUsersAsync(int id);
 
       Task<ServiceResult<Comment>> EditLikeOnComment(int commentId,int userId);

   
    }
}
