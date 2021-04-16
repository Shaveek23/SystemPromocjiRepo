using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.POCO;
using WebApi.Services;
namespace WebApi.Database.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public ServiceResult<IQueryable<Category>> GetCategoriesSubscribedByUser(int userID);

    }
}