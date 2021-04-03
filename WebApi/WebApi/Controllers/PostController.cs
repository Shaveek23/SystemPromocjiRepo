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
            if (result != null) return Ok(result);
            else return NotFound();
        }


        // Czy skoro userID Jest z Query to nie powinno być :  [HttpGet]
        [HttpGet("{userID}")]
        public ActionResult<IQueryable<PostDTO>> GetUserPosts([FromQuery] int UserID) //[FromHeader] int headerID
        {
            var result = _postService.GetAllOfUser(UserID);
            if (result != null) return Ok(result);
            else return NotFound();
        }

        [HttpGet("{postID}")]
        public ActionResult<PostDTO> Get([Required][FromHeader] int userID, [FromRoute] int id)
        {
            var result = _postService.GetById(id);
            if (result != null) return Ok(result);
            else return NotFound();
        }


        [HttpDelete("{postID}")]
        public ActionResult<Task> Delete([Required][FromHeader] int userID, [FromRoute] int postID, [FromBody] DateTime dateTime)
        {
            var result = _postService.DeletePostAsync(postID);
            if (result != null) return Ok(result);
            else return NotFound();
        }

        [HttpPut("{postID}")]
        public ActionResult<Task> Edit([Required][FromHeader] int userID, [FromRoute] int postID, [FromBody] PostEditDTO body)
        {
            var result = _postService.EditPostAsync(postID, body);
            if (result != null) return Ok(result);
            else return NotFound();
        }

        [HttpPost("{postID}")]
        public ActionResult<Task<int>> Create([Required][FromHeader] int userID, [FromBody] PostEditDTO body) //NO USERID IN DOCUMENTATION, discuss with other groups
        {
            var result = _postService.AddPostAsync(body, userID);
            if (result != null) return Ok(result);
            else return NotFound();
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
