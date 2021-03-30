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
        public IQueryable<PostDTO> GetAll([FromHeader] int headerID)
        {
            return _postService.GetAll();
        }

        [HttpGet("{UserID}")]
        public PostDTO GetUserPosts(int UserID) //[FromHeader] int headerID
        {
            return _postService.GetById(UserID);
        }

        [HttpGet("/post/{postID}")]
        public PostDTO Get([FromHeader] int headerID, int postID)
        {
            return _postService.GetById(postID);
        }


        [HttpDelete("/post/{postID}")]
        public PostDTO Delete([FromHeader] int headerID, int postID)
        {
            return _postService.GetById(postID);
        }

        [HttpPut("/post/{postID}")]
        public PostDTO Edit([FromHeader] int headerID, int PostID)
        {
            return _postService.GetById(PostID);
        }

        [HttpPost("/post")]
        public PostDTO Create(int PostID)
        {
            return _postService.GetById(PostID);
        }

        //I need Comment DTO to implement this endpoint
        [HttpGet("/post/{postID}/comments")]
        public void GetPostComments(int PostID)
        {
            //return _postService.GetAllComments(PostID);
        }

        //I need Like-Post Table in database
        [HttpGet("/post/{postID}/likeUsers")]
        public PostLikesDTO GetPostLikes(int PostID)
        {
            //return _postService.GetAllLikes(PostID);
            return new PostLikesDTO{ likers=null };
        }

        [HttpPut("/post/{postID}/likeUsers")]
        public void EditLikeStatus(int PostID)
        {
            //return _postService.EditLikeStatus(PostID);
        }

    }
}
