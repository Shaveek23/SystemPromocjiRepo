using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Models.DTO;
using WebApi.Services.Services_Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
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
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentDTO))]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async  Task<ActionResult> AddComment([FromBody] CommentDTO comment, [FromHeader] int userId)
        {
            var result = await _commentService.AddCommentAsync(userId, comment);
            if (result == null) return NotFound();
            return Ok( result.CommentID);
        
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
