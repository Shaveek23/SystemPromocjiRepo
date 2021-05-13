using WebApi.Models.DTO.PostDTOs;

namespace WebApi.Services.Services_Implementations
{
    public interface INewsletterService
    {
        public void SendNewsletterNotifications(bool isPostAddedSuccessfuly, string title, int categoryId);
    }
}