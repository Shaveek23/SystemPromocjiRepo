using System;
using System.Collections.Generic;
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
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostController> _logger;
        public PostController(ILogger<PostController> logger , IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }


        [HttpGet]
        public IQueryable<PostDTO> GetAll([FromHeader] int userID)
        {
            return _postService.GetAll();
        }

        //Proponuje przenieść to do kontrolera user. Zniknie konflikt posts/userID i posts/postID
        [HttpGet("{userID}")]
        public IQueryable<PostDTO> GetUserPosts([FromQuery] int UserID) //[FromHeader] int headerID
        {
            return _postService.GetAllOfUser(UserID);
        }

        [HttpGet("{postID}")]
        public PostDTO Get([FromHeader] int userID, [FromRoute] int id)
        {
            return _postService.GetById(id);
        }


        [HttpDelete("{postID}")]
        public Task Delete([FromHeader] int userID, [FromRoute] int postID, [FromBody] DateTime dateTime)
        {
            return _postService.DeletePostAsync(postID);
        }

        [HttpPut("{postID}")]
        public Task Edit([FromHeader] int userID, [FromRoute] int postID, [FromBody] PostEditDTO body)
        {
            return _postService.EditPostAsync(postID, body);
        }

        [HttpPost("{postID}")]
        public Task<int> Create([FromHeader] int userID, [FromBody] PostEditDTO body) //NO USERID IN DOCUMENTATION, discuss with other groups
        {
            return _postService.AddPostAsync(body, userID);
        }


        //I need Comment DTO to implement this endpoint
        //[HttpGet("{postID}/comments")]
        //public IQueryable<CommentDTO> GetPostComments([FromHeader] int userID, [FromQuery] )
        //{
        //    return _postService.GetAllComments(PostID);
        //}


        //I need Like-Post Table in database
        [HttpGet("{postID}/likeUsers")]
        public IQueryable<int> GetPostLikes([FromRoute] int postID)
        {
            return _postService.GetLikes(postID);
        }

        [HttpPut("{postID}/likeUsers")]
        public Task EditLikeStatus([FromHeader] int userID, [FromRoute] int commentID, [FromBody] bool Like)
        {
            return _postService.EditLikeStatusAsync(commentID, Like);
        }

    }
}
