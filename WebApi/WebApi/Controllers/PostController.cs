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
            if (result == null) return NotFound();
            else return Ok(result);
        }

      
        [HttpGet("/ByUser/{UserID}")]
        public ActionResult<IQueryable<PostDTO>> GetUserPosts([Required][FromRoute] int UserID) 
        {
            var result = _postService.GetAllOfUser(UserID);
            if (result == null) return NotFound();
            else return Ok(result);
        }


        [HttpGet("{postID}")]
        public ActionResult<PostDTO> Get([Required][FromHeader] int userID, [FromRoute] int postID)
        {
            var result = _postService.GetById(postID);
            if (result == null) return NotFound();
            else return Ok(result);
        }


        [HttpDelete("{postID}")]
        public ActionResult<Task> Delete([Required][FromHeader] int userID, [FromRoute] int postID)//, [FromBody] DateTime dateTime) - nie wiem czemu z tym nie działa. Może trzeba zoribć DTO na datetime?
        {
            _postService.DeletePostAsync(postID);
            return Ok();
        }

        [HttpPut("{postID}")]
        public ActionResult<Task> Edit([Required][FromHeader] int userID, [FromRoute] int postID, [FromBody] PostEditDTO body)
        {
            var result = _postService.EditPostAsync(postID, body);
            if (result == null) return NotFound();
            else return Ok(result);
        }


        //TODO:
        //Trzeba napisać na grupe od specyfikcji że zmienił się endpoint
        //[HttpPost("{postID}")]
        //Zmieniłem na:
        [HttpPost]
        //I tak nie potrzebujemy postID w endpoincie, bo dopiero je tworzymy.
        public ActionResult<Task<int>> Create([Required][FromHeader] int userID, [FromRoute] int postID, [FromBody] PostEditDTO body) //NO USERID IN DOCUMENTATION, discuss with other groups
        {
            var result = _postService.AddPostAsync(body, userID);
            if (result == null) return NotFound();
            else return Ok(result);
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
