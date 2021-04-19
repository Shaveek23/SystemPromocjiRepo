using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models;
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

        public HomeController(ILogger<HomeController> logger, IWallService service, ICommentService commentService, IPostService postService)
        {
            _logger = logger;
            _service = service;
            _commentService = commentService;
            _postService = postService;
        }

        public async Task<IActionResult> WallAsync()
        {
            ServiceResult<WallViewModel> wall = await _service.getWall(1);
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
      
        public async  Task AddNewPost(string postText,int userId)
        {
            await _postService.AddNewPost(postText,userId);
        }
        public async Task AddNewComment(string commentText,int postId, int userId)
        {
            await _commentService.AddNewComment(commentText, postId,userId);
        }

    }
}
