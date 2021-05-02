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
            return new ControllerResult<CommentDTOOutput>(result).GetResponse();
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddComment([FromBody] CommentDTONew comment, [Required][FromHeader] int userId)
        {

            var result = await _commentService.AddCommentAsync(userId, comment);
            return new ControllerResult<int?>(result).GetResponse();

        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteComment([FromRoute] int id, [Required][FromHeader] int userId)
        {
            var result = _commentService.DeleteComment(id, userId);
            return new ControllerResult<bool>(result).GetResponse();

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> EditComment([FromRoute] int id, [Required][FromHeader] int userId, [FromBody] CommentDTOEdit comment)
        {
            var result = await _commentService.EditCommentAsync(id, userId, comment);
            return new ControllerResult<bool>(result).GetResponse();
        }


        [HttpGet("{id}/likeUsers")]
        public ActionResult<IQueryable<int>> GetPostLikes([Required][FromRoute] int id)
        {
            var result = _commentService.GetLikedUsers(id);
            return new ControllerResult<IQueryable<int>>(result).GetResponse();
        }

        [HttpPut("{id}/likeUsers")]
        public async Task<IActionResult> EditLikeStatus([Required][FromHeader] int userID, [FromRoute] int id, [FromBody] LikeDTO like)
        {
            var result = await _commentService.EditLikeOnCommentAsync(userID, id, like);
            return new ControllerResult<bool>(result).GetResponse();


        }

    }

}
