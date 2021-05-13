using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.DTO.PostDTOs;
using WebApi.Services.Hosted_Service;
using WebApi.Services.Services_Interfaces;

namespace WebApi.Services.Services_Implementations
{
    public class NewsletterService : INewsletterService
    {
        private readonly ISendingMonitorService _sendingMonitorService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        
        public NewsletterService(ISendingMonitorService sendingMonitorService, ICategoryService categoryService, IUserService userService)
        {
            _sendingMonitorService = sendingMonitorService;
            _categoryService = categoryService;
            _userService = userService;
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


        // TO DO:
        //tutaj dodać funckje do pobierania kategorii które subskrybuje użytkownik o id = x
    }
}
