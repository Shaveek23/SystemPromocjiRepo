using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models.DTO;
using WebApi.Models.DTO.PostDTOs;

namespace WebApi.Services.Services_Implementations
{
    public interface INewsletterService
    {
        public void SendNewsletterNotifications(bool isPostAddedSuccessfuly, string title, int categoryId);

        public ServiceResult<IQueryable<idDTO>> GetSubscribedCategories(int userID);

        public  Task<ServiceResult<bool>> SetSubscriptionAsync(NewsletterDTO dto, int userID, bool subscribe);

        public ServiceResult<List<int>> GetSubscribers(int categoryID);
    }
}