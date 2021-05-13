using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Mapper;
using WebApi.Database.Repositories.Implementations;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Models.DTO;
using WebApi.Models.DTO.PostDTOs;
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

            // TO DO:
            // tutaj należy wydobyć informacje o subskrybentach danej kategorii
            string[] mockedReceivers = new string[] { "abc@abc.pl", "anotherReceiver@gmail.com", "email@email.eu" }; // tutaj trzeba pozyskać subskrybentów z bazy -> endpoint w Newsletter ?
            string postTitle = title;
            string category = categoryId.ToString(); // tutaj trzeba wyszukać nazwę kategorii serwisem kategorii

            //

            _sendingMonitorService.Send(mockedReceivers, postTitle, category);
        }


        // TO DO:
        //tutaj dodać funckje do pobierania subskrybentów danej kategorii

        public ServiceResult<IQueryable<idDTO>> getSubscribedCategories(int userID)
        {
            var res = _newsletterRepository.GetAllSubscribedCategories(userID);
            return new ServiceResult<IQueryable<idDTO>>(Mapper.MapNewslettersToUserIds(res.Result));
        }
    }
}
