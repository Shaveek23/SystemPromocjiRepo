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
    public class PostController : Controller
    {
        private readonly ILogger<CommentController> _logger;
        private readonly IWallService _service;
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;

        public PostController(ILogger<CommentController> logger, IWallService service, ICommentService commentService, IPostService postService)
        {
            _logger = logger;
            _service = service;
            _commentService = commentService;
            _postService = postService;
        }


        public async Task<IActionResult> AddNewPost(string postText, int userId, int categoryId, string title)
        {
            var result = await _postService.AddNewPost(postText, userId, categoryId, title);
            if (result.Result)
                return View();
            else
                return View(new ErrorViewModel());
        }

        public async Task<IActionResult> EditPost(int userID, int postID, string title, string content, int categoryID, int isPromoted)
        {
            bool ispromoted = isPromoted == 1 ? true : false;
            var result = await _postService.EditPost(userID, postID, title, content, categoryID, ispromoted);
            if (result.Result)
                return View();
            else
                return View(new ErrorViewModel());
        }

        public async Task<IActionResult> DeletePost(int userID, int postID)
        {
            var result = await _postService.DeletePost(userID, postID);
            if (result.Result)
                return RedirectToActionPermanent("UserWall", "Home" , new { userID = userID });
            else
                return View(new ErrorViewModel());
        }

        public async Task<IActionResult> EditPostLikeStatus(int postID, int userID, bool like)
        {
            var result = await _postService.EditLikeStatus(postID, userID, like);
            if (result.Result)
                return View();
            else
                return View(new ErrorViewModel());
        }


    }
}
