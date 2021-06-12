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

        public UserInterfaceController(ILogger<UserInterfaceController> logger, IUserService userService, IUserInterfaceService userInterfaceService)
        {
            _logger = logger;
            _userService = userService;
            _userInterfaceService = userInterfaceService;
        }


        public async Task<IActionResult> EditUser(int userID, string userName, string userEmail)
        {
            var result = await _userService.EditUser(userID, userName, userEmail);
            if (result.Result)
                return View();
            else
                return View(new ErrorViewModel());
        }
        
        public async Task<IActionResult> SubscribeCategory(int userID, int categoryID, bool subscribe)
        {
            var result = await _userInterfaceService.SubscribeCategory(userID, categoryID, subscribe);
            if (result.Result)
                return View();
            else
                return View(new ErrorViewModel());
        }



    }
}
