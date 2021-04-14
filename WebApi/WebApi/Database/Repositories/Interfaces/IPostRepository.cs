using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.DTO.PostDTOs;
using WebApi.Models.POCO;
using WebApi.Services;

namespace WebApi.Database.Repositories.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {

        ServiceResult<IQueryable<Comment>> GetAllComments(int postID);
    }
}
