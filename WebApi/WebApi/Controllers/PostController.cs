using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Models.DTO;
using WebApi.Models.DTO.PostDTOs;
using WebApi.Services.Services_Interfaces;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostController> _logger;
        public PostController(ILogger<PostController> logger, IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }


        [HttpGet]
        public ActionResult<IQueryable<PostDTO>> GetAll([Required][FromHeader] int userID)
        {
            var result = _postService.GetAll();
            return new ControllerResult<IQueryable<PostDTO>>(result).GetResponse();
        }


        [HttpGet("byUser/{UserID}")]
        public ActionResult<IQueryable<PostDTO>> GetUserPosts([Required][FromRoute] int UserID)  // [Required][FromHeader] int userID ??
        {
            var result = _postService.GetAllOfUser(UserID);
            return new ControllerResult<IQueryable<PostDTO>>(result).GetResponse();
        }


        [HttpGet("{postID}")]
        public ActionResult<PostDTO> Get([Required][FromHeader] int userID, [FromRoute] int postID)
        {
            var result = _postService.GetById(postID, userID);
            return new ControllerResult<PostDTO>(result).GetResponse();
        }


        [HttpDelete("{postID}")]
        public async Task<IActionResult> Delete([Required][FromHeader] int userID, [FromRoute] int postID)
        {
            var result = await _postService.DeletePostAsync(postID);
            return new ControllerResult<bool>(result).GetResponse();
        }

        [HttpPut("{postID}")]
        public async Task<IActionResult> Edit([Required][FromHeader] int userID, [FromRoute] int postID, [FromBody] PostEditDTO body)
        {
            var result = await _postService.EditPostAsync(postID, body);
            return new ControllerResult<bool>(result).GetResponse();
        }


        //TODO:
        //Trzeba napisać na grupe od specyfikcji że zmienił się endpoint
        // z [HttpPost("{postID}")] - błąd w dokumentacji
        [HttpPost]
        public async Task<IActionResult> Create([Required][FromHeader] int userID, [FromBody] PostEditDTO body) //NO USERID IN DOCUMENTATION, discuss with other groups
        {
            var result = await _postService.AddPostAsync(body, userID);
            return new ControllerResult<int?>(result).GetResponse();
        }


        [HttpGet("{postID}/comments")]
        public ActionResult<IQueryable<CommentDTOOutput>> GetPostComments([Required][FromHeader] int userID, [Required][FromRoute] int postID)
        {
            var result = _postService.GetAllComments(postID, userID);
            return new ControllerResult<IQueryable<CommentDTOOutput>>(result).GetResponse();
        }


        [HttpGet("{postID}/likeUsers")]
        public ActionResult<IQueryable<int>> GetPostLikes([Required][FromRoute] int postID)
        {
            var result = _postService.GetLikes(postID);
            return new ControllerResult<IQueryable<int>>(result).GetResponse();
        }

        [HttpPut("{postID}/likeUsers")]
        public async Task<IActionResult> EditLikeStatus([Required][FromHeader] int userID, [FromRoute] int postID, [FromBody] LikeDTO like)
        {
            var result = await _postService.EditLikeStatusAsync(userID, postID, like);
            return new ControllerResult<bool>(result).GetResponse();
        }

    }
}
