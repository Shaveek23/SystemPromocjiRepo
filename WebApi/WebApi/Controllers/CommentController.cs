using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Models.DTO;
using WebApi.Services.Services_Interfaces;





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
        public ActionResult<IQueryable<CommentDTOOutput>> GetAll([Required][FromHeader] int userId)
        {
            var result = _commentService.GetAll(userId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<CommentDTOOutput> GetById([FromRoute] int id, [Required][FromHeader] int userId)
        {
            var result = _commentService.GetById(id, userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddComment([FromBody] CommentDTO comment, [Required][FromHeader] int userId)
        {

            var result = await _commentService.AddCommentAsync(userId, comment);
            return Ok(result.CommentID);

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteComment([FromRoute] int id, [Required][FromHeader] int userId)
        {
            _commentService.DeleteComment(id, userId);
            return Ok();

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditComment([FromRoute] int id, [Required][FromHeader] int userId, [FromBody] CommentDTO comment)
        {
            await _commentService.EditCommentAsync(id, userId, comment);
            return Ok();
        }

        #region TO DO: implement those endpoints
        //[HttpGet("{id}/likedUsers")]
        //public ActionResult<IQueryable<int>> GetLikedUsers([FromRoute] int id)
        //{
        //    var result = _commentService.GetLikedUsers(id);
        //    return Ok(result);
        //}

        //[HttpPut("{id}/likedUsers")]
        //public async Task<ActionResult> EditLikeOnComment([FromRoute] int id, [Required][FromHeader] int userId)
        //{
        //    await _commentService.EditLikeOnCommentAsync(id, userId);
        //    return Ok();
        //}
        #endregion

    }

}
