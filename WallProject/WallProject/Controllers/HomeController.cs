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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWallService _service;
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IWallService service, ICommentService commentService, IPostService postService, IUserService userService)
        {
            _logger = logger;
            _service = service;
            _commentService = commentService;
            _postService = postService;
            _userService = userService;
        }


        public async Task<IActionResult> WallAsync()
        {
            ServiceResult<WallViewModel> wall = await _service.getWall(140);
            if (wall.IsOk())
                return View(wall.Result);
            else
                return View("Privacy");
        }

        [Route("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("getWall/{userID}")]
        public async Task<IActionResult> UserWall([FromRoute] int userID)
        {
            ServiceResult<WallViewModel> wall = await _service.getWall(userID);
            if (wall.IsOk())
                return View(wall.Result);
            else
                return View("Privacy", wall.Message);
        }

        [Route("getUserInterface/{userID}")]
        public async Task<IActionResult> UserInterface([FromRoute] int userID)
        {
            ServiceResult<UserViewModel> wall = await _userService.getById(userID);
            if (wall.IsOk())
                return View("UserInterface", wall.Result);
            else
                return View("Privacy", wall.Message);
        }


        [Route("userPosts/{userID}")]
        public async Task<IActionResult> UserPosts([FromRoute] int userID)
        {
            ServiceResult<WallViewModel> wall = await _service.getUserPosts(userID);
            if (wall.IsOk())
                return View(wall.Result);
            else
                return View("Privacy", wall.Message);
        }

        [Route("userComments/{userID}")]
        public async Task<IActionResult> UserComments([FromRoute] int userID)
        {
            ServiceResult<CommentListViewModel> wall = await _service.getUserComments(userID);
            if (wall.IsOk())
                return View(wall.Result);
            else
                return View("Privacy", wall.Message);
        }

        [Route("showPost/{postID}")]
        public async Task<IActionResult> ShowPost([FromRoute] int postID, int userID)
        {
            ServiceResult<PostEditViewModel> postVM = await _service.getPostToEdit(userID, postID);
            if (postVM.IsOk())
                return View(postVM.Result);
            else
                return View("Privacy", postVM.Message);
        }


        public async Task<IActionResult> ChangeCategoryFilterStatus(int categoryID)
        {
            _service.ChangeCategoryFilterStatus(categoryID);
            ServiceResult<WallViewModel> wall = await _service.getWall(1);
            if (wall.IsOk())
            {
                return View(wall.Result);
            }
            else
                return View("Privacy", wall.Message);
        }
    }

}
