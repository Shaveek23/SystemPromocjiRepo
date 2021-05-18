using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Database
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        ServiceResult<IQueryable<TEntity>> GetAll();

        ServiceResult<TEntity> GetById(int id);

        Task<ServiceResult<TEntity>> AddAsync(TEntity entity);

        Task<ServiceResult<TEntity>> UpdateAsync(TEntity entity, int userID);

        Task<ServiceResult<TEntity>> RemoveAsync(TEntity entity, int userID);

        ServiceResult<bool> Delete(int entityID, int userID);
    }
}
