using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Exceptions;

namespace WebApi.Database
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly DatabaseContext dbContext;

        public Repository(DatabaseContext databaseContext)
        {
            dbContext = databaseContext;
        }

        // TODO:
        // WYJĄTKI NIE SĄ RZUCANE !!!
        public TEntity GetById(int id)
        {
                var result = dbContext.Find<TEntity>(id);
                if (result == null)
                    throw new ResourceNotFoundException("Requested resource has not been found.");

                return result;
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new AddAsyncFailedException($"{nameof(AddAsync)} entity must not be null");


            try
            {
                await dbContext.AddAsync(entity);
                await dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {
                throw new AddAsyncFailedException($"Fail when adding a new {nameof(AddAsync)} resource item");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new UpdateAsyncFailException($"{nameof(UpdateAsync)} entity must not be null");
                
            }

            try
            {

                dbContext.Update(entity);
                await dbContext.SaveChangesAsync();

                return entity;
            }
            catch
            {
                throw new UpdateAsyncFailException($"Fail when updating a {nameof(UpdateAsync)} resource item");
            }
        }


        public async Task<TEntity> RemoveAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be removed: {ex.Message}");
            }
        }
    }
}
