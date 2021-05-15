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

        Task<ServiceResult<TEntity>> UpdateAsync(TEntity entity);

        Task<ServiceResult<TEntity>> RemoveAsync(TEntity entity);

        ServiceResult<bool> Delete(int entityID);
    }
}
