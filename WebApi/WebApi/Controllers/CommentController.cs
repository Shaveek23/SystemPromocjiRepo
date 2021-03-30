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
        public IQueryable<CommentDTO> GetAll()
        {
            return _commentService.GetAll();
        }
        [HttpGet("{id}")]
        public CommentDTO GetById( int id)
        {
            return _commentService.GetById(id);
        }
        [HttpGet("{id}/likedUsers")]
        public IQueryable<int> GetLikedUsers([FromQuery] int id)
        {
            return _commentService.GetLikedUsers(id);
        }
        [HttpPost]
        public async Task<CommentDTO> AddComment( [FromBody] CommentDTO comment)
        {
            return await _commentService.AddCommentAsync(comment);
        }
        [HttpDelete("{id}")]
        public  void DeleteComment( int id)
        {
            _commentService.DeleteComment(id);
        }
        [HttpPut]
        public async Task<CommentDTO> EditComment( [FromBody] CommentDTO comment)
        {
            return await _commentService.EditCommentAsync(comment);
        }
        [HttpPut]
        public async Task EditLikeOnComment( [FromQuery] int commentId)
        {
            await _commentService.EditLikeOnCommentAsync( commentId);
        }


    }
}
