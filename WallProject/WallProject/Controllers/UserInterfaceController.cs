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

        public UserInterfaceController(ILogger<UserInterfaceController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [Route("getUserInterface/{userID}")]
        public async Task<IActionResult> UserInterface([FromRoute] int userID)
        {
            ServiceResult<UserViewModel> user = await _userService.getById(userID);
            if (user.IsOk())
                return View(user.Result);
            else
                return View("Privacy");
        }
        public async Task<IActionResult> EditUser(int userID, string userName, string userEmail)
        {
            var result = await _userService.EditUser(userID, userName, userEmail);
            if (result.Result)
                return View();
            else
                return View(new ErrorViewModel());
        }

    }
}
