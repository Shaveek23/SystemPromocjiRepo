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
        public PostController(ILogger<PostController> logger , IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }


        [HttpGet]
        public ActionResult<IQueryable<PostDTO>> GetAll([Required][FromHeader] int userID)
        {
            var result = _postService.GetAll();
            return Ok(result);
        }

      
        [HttpGet("byUser/{UserID}")]
        public ActionResult<IQueryable<PostDTO>> GetUserPosts([Required][FromRoute] int UserID)  // [Required][FromHeader] int userID ??
        {
            var result = _postService.GetAllOfUser(UserID);
            return Ok(result);
        }


        [HttpGet("{postID}")]
        public ActionResult<PostDTO> Get([Required][FromHeader] int userID, [FromRoute] int postID)
        {
            var result = _postService.GetById(postID);
            return Ok(result);
        }


        [HttpDelete("{postID}")]
        public async Task<IActionResult> Delete([Required][FromHeader] int userID, [FromRoute] int postID)
        {
            await _postService.DeletePostAsync(postID);
            return Ok();
        }

        [HttpPut("{postID}")]
        public async Task<IActionResult> Edit([Required][FromHeader] int userID, [FromRoute] int postID, [FromBody] PostEditDTO body)
        {
            var result = await _postService.EditPostAsync(postID, body);
            return Ok();
        }


        //TODO:
        //Trzeba napisać na grupe od specyfikcji że zmienił się endpoint
        // z [HttpPost("{postID}")] - błąd w dokumentacji
        [HttpPost]
        public async Task<IActionResult> Create([Required][FromHeader] int userID, [FromBody] PostEditDTO body) //NO USERID IN DOCUMENTATION, discuss with other groups
        {
            var result = await _postService.AddPostAsync(body, userID);
            return Ok(result);
        }


        //TODO:

        //I need Comment DTO to implement this endpoint
        //[HttpGet("{postID}/comments")]
        //public IQueryable<CommentDTO> GetPostComments([FromHeader] int userID, [FromQuery] )
        //{
        //    return _postService.GetAllComments(PostID);
        //}


        //I need Like-Post Table in database
        //[HttpGet("{postID}/likeUsers")]
        //public IQueryable<int> GetPostLikes([Required][FromRoute] int postID)
        //{
        //    return _postService.GetLikes(postID);
        //}

        //[HttpPut("{postID}/likeUsers")]
        //public Task EditLikeStatus([Required][FromHeader] int userID, [FromRoute] int commentID, [FromBody] bool Like)
        //{
        //    return _postService.EditLikeStatusAsync(commentID, Like);
        //}

    }
}
