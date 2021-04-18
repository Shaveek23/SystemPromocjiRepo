using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Models.POCO;
using WebApi.Services;

namespace WebApi.Database.Repositories.Implementations
{
    public class CategoryRepository: Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext databaseContext) : base(databaseContext) { }
        public ServiceResult<IQueryable<Category>> GetCategoriesSubscribedByUser(int userID)
        {

            //Tak to bedzie wygladalo jak zrobimy newslettera

            //var newstletters = dbContext.Newsletters.Where(newstletters => newstletters.UserID = userID);
            //List<Category> categories = new List<Category>();
            //foreach(var newsletter in newstletters)
            //{
            //    categories.Add(dbContext.Categories.Where(category => category.CategoryID == newsletter.CategoryID));
            //}

            //return new ServiceResult<IQueryable<Category>>(categories.AsQueryable());


            throw new NotImplementedException();
        }

    }
}
