﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Models.DTO;
using WebApi.Models.DTO.PostDTOs;
using WebApi.Services.Hosted_Service;
using WebApi.Services.Services_Implementations;
using WebApi.Services.Services_Interfaces;


namespace WebApi.Controllers
{
    [ApiController]

    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostController> _logger;
        private readonly INewsletterService _newsletterService;

        public PostController(ILogger<PostController> logger, IPostService postService, INewsletterService newsletterService)
        {
            _logger = logger;
            _postService = postService;
            _newsletterService = newsletterService;
        }


        [HttpGet("posts")]
        public ActionResult<IQueryable<PostDTOOutput>> GetAll([Required][FromHeader] int userID)
        {
            var result = _postService.GetAll(userID);
            return new ControllerResult<IQueryable<PostDTOOutput>>(result).GetResponse();
        }


        [HttpGet("posts/{UserID}")]
        public ActionResult<IQueryable<PostDTOOutput>> GetUserPosts([Required][FromRoute] int UserID)  // [Required][FromHeader] int userID ??
        {
            var result = _postService.GetAllOfUser(UserID);
            return new ControllerResult<IQueryable<PostDTOOutput>>(result).GetResponse();
        }


        [HttpGet("post/{postID}")]
        public ActionResult<PostDTOOutput> Get([Required][FromHeader] int userID, [FromRoute] int postID)
        {
            var result = _postService.GetById(postID, userID);
            return new ControllerResult<PostDTOOutput>(result).GetResponse();
        }


        [HttpDelete("post/{postID}")]
        public async Task<IActionResult> Delete([Required][FromHeader] int userID, [FromRoute] int postID)
        {
            var result = await _postService.DeletePostAsync(postID);
            return new ControllerResult<bool>(result).GetResponse();
        }

        [HttpPut("post/{postID}")]
        public async Task<IActionResult> Edit([Required][FromHeader] int userID, [FromRoute] int postID, [FromBody] PostDTOEdit body)
        {
            var result = await _postService.EditPostAsync(postID, body);
            return new ControllerResult<bool>(result).GetResponse();
        }


        [HttpPost("post")]
        public async Task<IActionResult> Create([Required][FromHeader] int userID, [FromBody] PostDTOCreate body) //NO USERID IN DOCUMENTATION, discuss with other groups
        {
            var result = await _postService.AddPostAsync(body, userID);
            _newsletterService.SendNewsletterNotifications(result.IsOk(), body.title, body.category.Value);
            return new ControllerResult<int?>(result).GetResponse();
        }


        [HttpGet("post/{postID}/comments")]
        public ActionResult<IQueryable<CommentDTOOutput>> GetPostComments([Required][FromHeader] int userID, [Required][FromRoute] int postID)
        {

            var result = _postService.GetAllComments(postID, userID);

            return new ControllerResult<IQueryable<CommentDTOOutput>>(result).GetResponse();
        }


        [HttpGet("post/{postID}/likedUsers")]
        public ActionResult<IQueryable<LikerDTO>> GetPostLikes([Required][FromRoute] int postID)
        {
            var result = _postService.GetLikes(postID);
            return new ControllerResult<IQueryable<LikerDTO>>(result).GetResponse();
        }

        [HttpPut("post/{postID}/likedUsers")]
        public async Task<IActionResult> EditLikeStatus([Required][FromHeader] int userID, [FromRoute] int postID, [FromBody] LikeDTO like)
        {
            var result = await _postService.EditLikeStatusAsync(userID, postID, like);
            return new ControllerResult<bool>(result).GetResponse();


        }

    }
}
