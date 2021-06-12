using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models.MainView;
using WallProject.Services.Services_Interfaces;

namespace WallProject.Services.Serives_Implementations
{
    public class UserInterfaceService:IUserInterfaceService
    {
        private readonly IUserService _userService; 
        private readonly ICategoryService _categoryService; 
        private readonly INewsletterService _newsletterService;
        public UserInterfaceService(IUserService userService, INewsletterService newsletterService, ICategoryService categoryService)
        {
            _userService = userService;
            _categoryService = categoryService;
            _newsletterService = newsletterService;
        }

        public async Task<ServiceResult<UserInterfaceView>> getUserInterface(int userID)
        { 
            var user = await _userService.getById(userID);
            if (!user.IsOk()) return new ServiceResult<UserInterfaceView>(null, user.Code, user.Message);
            var allCategories = await _categoryService.getAll();
            if (!allCategories.IsOk()) return new ServiceResult<UserInterfaceView>(null, allCategories.Code, allCategories.Message);
            var subscribedCategoriesID = await _userService.getAllSubscribedCategoriesID(userID);
            if (!subscribedCategoriesID.IsOk()) return new ServiceResult<UserInterfaceView>(null, subscribedCategoriesID.Code, subscribedCategoriesID.Message);

            UserInterfaceView view = new UserInterfaceView { User = user.Result, AllCategories = allCategories.Result, SubscribedCategoriesID = subscribedCategoriesID.Result };
            return new ServiceResult<UserInterfaceView>(view, System.Net.HttpStatusCode.OK, null);
        }

        public Task<ServiceResult<bool>> SubscribeCategory(int userID, int categoryID, bool Subscribe)
        {
           return _newsletterService.subscribeCategory(userID, categoryID, Subscribe);
        }

    }
}
