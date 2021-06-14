using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models;
using WallProject.Models.MainView;
using WallProject.Services;
using WallProject.Services.Services_Interfaces;
namespace WallProject.Controllers
{
    public class UserInterfaceController : Controller
    {
        private readonly ILogger<UserInterfaceController> _logger;
        private readonly IUserService _userService;
        private readonly IUserInterfaceService _userInterfaceService;
        private readonly IWallService _wallService;

        public UserInterfaceController(ILogger<UserInterfaceController> logger, IUserService userService, IUserInterfaceService userInterfaceService, IWallService wallService)
        {
            _logger = logger;
            _userService = userService;
            _userInterfaceService = userInterfaceService;
            _wallService = wallService;
        }


        public async Task<IActionResult> EditUser(int userID, string userName, string userEmail)
        {
            var result = await _userService.EditUser(userID, userName, userEmail);
            var second_result = await _wallService.getWall(userID);
            if (result.Result && second_result.IsOk())
                return View("UserWall",second_result.Result);
            else
                return View(new ErrorViewModel());
        }
        
        public async Task<IActionResult> SubscribeCategory(int userID, int categoryID, int subscribe)
        {
            bool subscription = true;
            if (subscribe == 0)
                subscription = false;
            var result = await _userInterfaceService.SubscribeCategory(userID, categoryID, subscription);
            var second_result = await _wallService.getWall(userID);
            if (result.Result && second_result.IsOk())
                return View("UserWall", second_result.Result);
            else
                return View(new ErrorViewModel());
        }



    }
}
