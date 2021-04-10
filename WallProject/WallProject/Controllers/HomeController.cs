using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models;
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
            WallViewModel wall = await _service.getWall(1);
            return View(wall);
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
            WallViewModel wall = await _service.getWall(userID);
            return View(wall);
        }

    }
}
