using System.Linq;
using WebApi.Models.POCO;
using WebApi.Services;

namespace WebApi.Database.Repositories.Implementations
{
    public interface INewsletterRepository : IRepository<Newsletter>
    {
        public ServiceResult<IQueryable<Newsletter>> GetAllSubscribedCategories(int userID);
    }
}