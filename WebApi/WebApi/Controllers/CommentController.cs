using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Models.DTO;
using WebApi.Services.Services_Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController
    {
        private readonly ICommentService _commentService;
        private readonly ILogger<CommentController> _logger;
        public CommentController(ILogger<CommentController> logger, ICommentService commentService)
        {
            _commentService = commentService;
            _logger = logger;
        }
        [HttpGet]
        public IQueryable<CommentDTO> GetAll([FromHeader] int userId)
        {
            return _commentService.GetAll(userId);
        }
        [HttpGet("{id}")]
        public CommentDTO GetById([FromRoute] int id, [FromHeader] int userId)
        {
            return _commentService.GetById(id,userId);
        }
       //Nw jak w post ma byc juz id 
        [HttpPost]
        public async Task<CommentDTO> AddComment([FromBody] CommentDTO comment, [FromHeader] int userId)
        {
            return await _commentService.AddCommentAsync(userId,comment);
        }
        [HttpDelete("{id}")]
        public  void DeleteComment( [FromRoute] int id,[FromHeader] int userId)
        {
            _commentService.DeleteComment(id,userId);
        }
        [HttpPut("{id}")]
        public async Task<CommentDTO> EditComment([FromRoute] int id, [FromHeader] int userId, [FromBody] CommentDTO comment)
        {
            return await _commentService.EditCommentAsync(id,userId,comment);
        }
        [HttpGet("{id}/likedUsers")]
        public IQueryable<int> GetLikedUsers([FromRoute] int id)
        {
            return _commentService.GetLikedUsers(id);
        }
        [HttpPut("{id}/likedUsers")]
        public async Task EditLikeOnComment([FromRoute] int id, [FromHeader] int userId)
        {
            await _commentService.EditLikeOnCommentAsync( id,userId);
        }


    }
}
