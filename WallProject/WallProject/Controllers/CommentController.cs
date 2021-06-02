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
    public class CommentController : Controller
    {
        private readonly ILogger<CommentController> _logger;
        private readonly IWallService _service;
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;

        public CommentController(ILogger<CommentController> logger, IWallService service, ICommentService commentService, IPostService postService)
        {
            _logger = logger;
            _service = service;
            _commentService = commentService;
            _postService = postService;
        }


        public async Task<IActionResult> AddNewComment(string commentText, int postId, int userId)
        {
            var result = await _commentService.AddNewComment(commentText, postId, userId);
            if (result.Result)
                return View();
            else
                return View(new ErrorViewModel());
        }

        public async Task<IActionResult> EditComment(int userID,int commentID, string content)
        {
            var result = await _commentService.Edit(userID, commentID, content);
            if (result.Result)
                return View();
            else
                return View(new ErrorViewModel());
        }

        public async Task<IActionResult> EditCommentLikeStatus(int commentID, int userID, bool like)
        {
            var result = await _commentService.EditLikeStatus(commentID, userID, like);
            if (result.Result)
                return View();
            else
                return View(new ErrorViewModel());
        }
    }
}
