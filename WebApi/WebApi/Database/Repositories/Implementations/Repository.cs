using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Database
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly DatabaseContext dbContext;

        public Repository(DatabaseContext databaseContext)
        {
            dbContext = databaseContext;
        }

        public TEntity GetById(int id)
        {
            try
            {


                var result = dbContext.Find<TEntity>(id);

                if (result != null) return result;
                throw new Exception();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return dbContext.Set<TEntity>();
            }
            catch
            {
                return null;

            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {

                await dbContext.AddAsync(entity);
                await dbContext.SaveChangesAsync();

                return entity;
            }

            catch (Exception ex)
            {


                return null;

            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                //throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
                return null;
            }

            try
            {

                dbContext.Update(entity);
                await dbContext.SaveChangesAsync();

                return entity;
            }
            catch
            {
                return null;
            }
        }
    }
}
