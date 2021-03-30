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
    [Route("api")]
    public class PostController : Controller
    {
        private readonly IPostService _postService; // this service handles all logic and database updates -> no logic in controllers !!!

        public PostController(IPostService postService)
        {
            _postService = postService;
        }


        [HttpGet]
        public IQueryable<PostDTO> GetAll([FromHeader] int userID)
        {
            return _postService.GetAll();
        }

        [HttpGet("{userID}")]
        public IQueryable<PostDTO> GetUserPosts([FromQuery] int UserID) //[FromHeader] int headerID
        {
            return _postService.GetAllOfUser(UserID);
        }

        [HttpGet("{postID}")]
        public PostDTO Get([FromHeader] int userID, [FromQuery] int id)
        {
            return _postService.GetById(id);
        }


        [HttpDelete("{postID}")]
        public void Delete([FromHeader] int userID, [FromQuery] int id, [FromBody] DateTime dateTime)
        {
            _postService.DeletePost(id);
        }

        [HttpPut("{postID}")]
        public void Edit([FromHeader] int userID, [FromQuery] int id, [FromBody] PostEditDTO body)
        {
            _postService.EditPost(id, body);
        }

        [HttpPost("{postID}")]
        public int Create([FromBody] PostEditDTO body)
        {
            return _postService.CreatePost(body);
        }

        //I need Comment DTO to implement this endpoint
        //[HttpGet("{postID}/comments")]
        //public IQueryable<CommentDTO> GetPostComments([FromHeader] int userID, [FromQuery] )
        //{
        //    return _postService.GetAllComments(PostID);
        //}

        //I need Like-Post Table in database
        [HttpGet("/post/{postID}/likeUsers")]
        public PostLikesDTO GetPostLikes([FromQuery] int postID)
        {
            return _postService.GetLikes(postID);
        }

        [HttpPut("/post/{postID}/likeUsers")]
        public void EditLikeStatus([FromHeader] int userID, [FromQuery] int commentID, [FromBody] bool Like)
        {
            _postService.EditLikeStatus(commentID, Like);
        }

    }
}
