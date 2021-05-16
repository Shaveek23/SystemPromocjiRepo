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

    public class CommentController : ControllerBase

    {
        private readonly ICommentService _commentService;
        private readonly ILogger<CommentController> _logger;
        public CommentController(ILogger<CommentController> logger, ICommentService commentService)
        {
            _commentService = commentService;
            _logger = logger;
        }

        [HttpGet("comments")]
        public ActionResult<IQueryable<CommentDTOOutput>> GetAll([Required][FromHeader] int userId)
        {
            var result = _commentService.GetAll(userId);
            return new ControllerResult<IQueryable<CommentDTOOutput>>(result).GetResponse();
        }

        [HttpGet("comments/{id}")]
        public ActionResult<IQueryable<CommentDTOOutput>> GetAllUsersComments([Required][FromHeader] int userId, [Required][FromRoute] int id)
        {
            var result = _commentService.GetAllOfUser(id);
            return new ControllerResult<IQueryable<CommentDTOOutput>>(result).GetResponse();
        }

        [HttpGet("comment/{id}")]
        public ActionResult<CommentDTOOutput> GetById([FromRoute] int id, [Required][FromHeader] int userId)
        {
            var result = _commentService.GetById(id, userId);
            return new ControllerResult<CommentDTOOutput>(result).GetResponse();
        }

        [HttpPost("comment")]
        public async Task<ActionResult<idDTO>> AddComment([FromBody] CommentDTONew comment, [Required][FromHeader] int userId) 
        {

            var result = await _commentService.AddCommentAsync(userId, comment);
            return new ControllerResult<idDTO>(result).GetResponse();

        }

        [HttpDelete("comment/{id}")]
        public ActionResult<bool> DeleteComment([FromRoute] int id, [Required][FromHeader] int userId)
        {
            var result = _commentService.DeleteComment(id, userId);
            return new ControllerResult<bool>(result).GetResponse();

        }

        [HttpPut("comment/{id}")]
        public async Task<ActionResult<bool>> EditComment([FromRoute] int id, [Required][FromHeader] int userId, [FromBody] CommentDTOEdit comment)
        {
            var result = await _commentService.EditCommentAsync(id, userId, comment);
            return new ControllerResult<bool>(result).GetResponse();
        }


        [HttpGet("comment/{id}/likedUsers")]
        public ActionResult<IQueryable<LikerDTO>> GetCommentLikes([Required][FromRoute] int id)
        {
            var result = _commentService.GetLikedUsers(id);
            return new ControllerResult<IQueryable<LikerDTO>>(result).GetResponse();
        }

        [HttpPut("comment/{id}/likedUsers")]
        public async Task<IActionResult> EditLikeStatus([Required][FromHeader] int userID, [FromRoute] int id, [FromBody] LikeDTO like)
        {
            var result = await _commentService.EditLikeOnCommentAsync(userID, id, like);
            return new ControllerResult<bool>(result).GetResponse();
        }

    }

}
