using System.Linq;
using WebApi.Models.DTO;
using WebApi.Models.DTO.PostDTOs;

namespace WebApi.Services.Services_Implementations
{
    public interface INewsletterService
    {
        public void SendNewsletterNotifications(bool isPostAddedSuccessfuly, string title, int categoryId);

        public ServiceResult<IQueryable<idDTO>> getSubscribedCategories(int userID);
    }
}