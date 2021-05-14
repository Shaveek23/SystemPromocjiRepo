using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.POCO;
using WebApi.Services;

namespace WebApi.Database.Repositories.Implementations
{
    public class NewsletterRepository : Repository<Newsletter>, INewsletterRepository
    {
        public NewsletterRepository(DatabaseContext databaseContext) : base(databaseContext) { }

        public ServiceResult<IQueryable<Newsletter>> GetAllSubscribedCategories(int userID)
        {
            var res = dbContext.Newsletters.Where(p => p.UserID == userID);
            if (res == null)
            {
                return new ServiceResult<IQueryable<Newsletter>>((IQueryable<Newsletter>)(new List<Newsletter>()));
            }
            return new ServiceResult<IQueryable<Newsletter>>(res);
        }

    }
}
