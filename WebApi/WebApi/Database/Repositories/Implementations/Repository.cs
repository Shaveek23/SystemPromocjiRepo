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

                //if (result == null)
                //    throw new ResourceNotFoundException("Requested resource has not been found.");

                //return result;
        }

        public ServiceResult<IQueryable<TEntity>> GetAll()
        {
            return new ServiceResult<IQueryable<TEntity>>(dbContext.Set<TEntity>());
        }

        public async Task<ServiceResult<TEntity>> AddAsync(TEntity entity)
        {
            if (entity == null)
                return ServiceResult<TEntity>.GetEntityNullResult();
            //throw new AddAsyncFailedException($"{nameof(AddAsync)} entity must not be null");

            try
            {
                await dbContext.AddAsync(entity);
                await dbContext.SaveChangesAsync();

                return new ServiceResult<TEntity>(entity);
            }
            catch (Exception e)
            {
                return ServiceResult<TEntity>.GetInternalErrorResult();
                //throw new AddAsyncFailedException($"Fail when adding a new {nameof(AddAsync)} resource item");
            }
        }

        public async Task<ServiceResult<TEntity>> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                //throw new UpdateAsyncFailException($"{nameof(UpdateAsync)} entity must not be null");
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
                //throw new UpdateAsyncFailException($"Fail when updating a {nameof(UpdateAsync)} resource item");
                return ServiceResult<TEntity>.GetInternalErrorResult();
            }
        }


        public async Task<ServiceResult<TEntity>> RemoveAsync(TEntity entity)
        {
            if (entity == null)
            {
                //throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
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
                //throw new Exception($"{nameof(entity)} could not be removed: {ex.Message}");
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
