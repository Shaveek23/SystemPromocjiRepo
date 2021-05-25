using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Database.Mapper;
using WebApi.Database.Repositories.Implementations;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Models.DTO;
using WebApi.Models.DTO.PostDTOs;
using WebApi.Models.POCO;
using WebApi.Services.Hosted_Service;
using WebApi.Services.Services_Interfaces;

namespace WebApi.Services.Services_Implementations
{
    public class NewsletterService : INewsletterService
    {
        private readonly ISendingMonitorService _sendingMonitorService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly INewsletterRepository _newsletterRepository;

        public NewsletterService(ISendingMonitorService sendingMonitorService, ICategoryRepository categoryRepository, IUserRepository userRepository, INewsletterRepository newsletterRepository)
        {
            _sendingMonitorService = sendingMonitorService;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _newsletterRepository = newsletterRepository;
        }

        public void SendNewsletterNotifications(bool isPostAddedSuccessfuly, string title, int categoryId) // tutaj zamiast PostEditDTO chyba wystarczy tylko tytul oraz id kategorii
        {
            if (!isPostAddedSuccessfuly)
                return;


            var res = GetSubscribers(categoryId);
            if (!res.IsOk())
                return;

            List<int> subscribersIds = res.Result;

            var res2 = _userRepository.GetAll();
            if (!res2.IsOk())
                return;

            IQueryable<User> subscribers = res2.Result.Where(u => subscribersIds.Contains(u.UserID));

            List<ReceiverDTO> receivers = Mapper.Map(subscribers);

            string postTitle = title;
            var categoryRes = _categoryRepository.GetById(categoryId);

            if (!categoryRes.IsOk())
                return;

            string catName = categoryRes.Result.Name;

            _sendingMonitorService.Send(receivers, postTitle, catName);
        }


        public ServiceResult<List<int>> GetSubscribers(int categoryID)
        {
            var res = _newsletterRepository.GetAll();

            if (!res.IsOk())
                return new ServiceResult<List<int>>(null, res.Code, res.Message);

            var newsletters = res.Result.Where(n => n.CategoryID == categoryID);

            List<int> result = new List<int>();

            foreach (var n in newsletters)
            {
                result.Add(n.UserID);
            }

            return new ServiceResult<List<int>>(result);

        }

        public ServiceResult<IQueryable<idDTO>> GetSubscribedCategories(int userID)
        {
            var res = _newsletterRepository.GetAllSubscribedCategories(userID);
            return new ServiceResult<IQueryable<idDTO>>(Mapper.MapNewslettersToUserIds(res.Result));
        }

        public async Task<ServiceResult<bool>> SetSubscriptionAsync(NewsletterDTO dto, int userID, bool subscribe)
        {

            var res = _newsletterRepository.GetAllSubscribedCategories(userID);
            if (!res.IsOk())
                return new ServiceResult<bool>(false, res.Code, res.Message);

            IQueryable<Newsletter> newsletters = res.Result;

            bool isAlreadySubscribed = newsletters?.Any(n => n.CategoryID == dto.CategoryID.Value) ?? false;

            if (!(isAlreadySubscribed ^ subscribe)) // if true & true or false & false then return ok because no action need to be performed
                return new ServiceResult<bool>(true);


            if (subscribe == true)
            {
                var addRes = await _newsletterRepository.AddAsync(Mapper.Map(dto, userID));
                if (!addRes.IsOk())
                    return new ServiceResult<bool>(false, res.Code, res.Message);
            }
            else
            {
                Newsletter toBeRemoved = newsletters.Where(n => n.CategoryID == dto.CategoryID.Value).FirstOrDefault();
                var removeRes = await _newsletterRepository.RemoveAsync(toBeRemoved);

                if (!removeRes.IsOk())
                    return new ServiceResult<bool>(false, res.Code, res.Message);

            }

            return new ServiceResult<bool>(true);
        }
    }
}