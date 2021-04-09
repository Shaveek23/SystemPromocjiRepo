using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WallProject.Models;
using WallProject.Services.Services_Interfaces;


namespace WallProject.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ILogger<PostController> _logger;
        private readonly IPostService _postService;

        public PostController(ILogger<PostController> logger, IPostService service)
        {
            _logger = logger;
            _postService = service;
        }

        public async Task<List<PostViewModel>> GetByUserIdAsync(int userId) => await _postService.GetByUserIdAsync(userId);

        public async Task<PostViewModel> GetByPostIdAsync(int postId) => await _postService.GetByPostIdAsync(postId);

        public async Task<List<PostViewModel>> GetAllAsync() => await _postService.GetAllAsync();

    }
}