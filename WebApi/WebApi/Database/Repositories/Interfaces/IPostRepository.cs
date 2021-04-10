using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.DTO.PostDTOs;
using WebApi.Models.POCO;

namespace WebApi.Database.Repositories.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
       
        IQueryable<Comment> GetAllComments(int postID);
    }
}
