using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.Exceptions;
using WebApi.Services;

namespace WebApi.Database
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly DatabaseContext dbContext;

        public Repository(DatabaseContext databaseContext)
        {
            dbContext = databaseContext;
        }

        public ServiceResult<TEntity> GetById(int id)
        {
            var result = dbContext.Find<TEntity>(id);
            // use static factory methods when they are sufficient or create your custom results with constructor or add your 
            // factory methods to avoid code repeating 
            if (result == null)
                return ServiceResult<TEntity>.GetResourceNotFoundResult();

            return new ServiceResult<TEntity>(result);

        }

        public ServiceResult<IQueryable<TEntity>> GetAll()
        {
            return new ServiceResult<IQueryable<TEntity>>(dbContext.Set<TEntity>());
        }

        public async Task<ServiceResult<TEntity>> AddAsync(TEntity entity)
        {
            if (entity == null)
                return ServiceResult<TEntity>.GetEntityNullResult();

            try
            {
                await dbContext.AddAsync(entity);
                await dbContext.SaveChangesAsync();

                return new ServiceResult<TEntity>(entity);
            }
            catch (Exception e)
            {
                return ServiceResult<TEntity>.GetInternalErrorResult();
            }
        }

        public async Task<ServiceResult<TEntity>> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                return ServiceResult<TEntity>.GetEntityNullResult();
            }

            try
            {
                dbContext.Update(entity);
                await dbContext.SaveChangesAsync();

                return new ServiceResult<TEntity>(entity);
            }
            catch
            {
                return ServiceResult<TEntity>.GetInternalErrorResult();
            }
        }


        public async Task<ServiceResult<TEntity>> RemoveAsync(TEntity entity)
        {
            if (entity == null)
            {
                return ServiceResult<TEntity>.GetEntityNullResult();
            }

            try
            {
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();

                return new ServiceResult<TEntity>(entity);
            }
            catch (Exception ex)
            {
                return ServiceResult<TEntity>.GetInternalErrorResult();
            }
        }

        public ServiceResult<bool> Delete(int entityID,int userId)
        {
            try
            {
                dbContext.Remove(dbContext.Find<TEntity>(entityID));
                dbContext.SaveChanges();
                return new ServiceResult<bool>(true);
            }
            catch
            {
                return ServiceResult<bool>.GetEntityNullResult();
            }
        }
      
    }
}
