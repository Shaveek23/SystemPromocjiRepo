using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Mapper;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Models.DTO;
using WebApi.Models.POCO;
using WebApi.Services.Services_Interfaces;

namespace WebApi.Services.Serives_Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CommentDTO> AddCommentAsync(int userId, CommentDTO comment)
        {

            Comment newComment = Mapper.Map(comment);
            Comment createdComment = await _commentRepository.AddAsync(newComment);
            if (createdComment == null) return null;
            return Mapper.Map(createdComment);

        }

        public bool DeleteComment(int commentId, int userId)
        {
            return _commentRepository.DeleteComment(commentId, userId);
        }

        public async Task<CommentDTO> EditCommentAsync(int commentId, int userId, CommentDTO comment)
        {
            Comment newComment = Mapper.Map(comment);
            newComment.CommentID = commentId;
            Comment editedComment = await _commentRepository.UpdateAsync(newComment);
            if (editedComment == null) return null;
            return Mapper.Map(editedComment);
        }

        public Task<bool> EditLikeOnCommentAsync(int commentId, int userId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CommentDTO> GetAll(int userId)
        {
            var result = _commentRepository.GetAll();
            if (result == null) return null;
            return Mapper.Map(result);
        }

        public CommentDTO GetById(int commentId, int userId)
        {

            var result = _commentRepository.GetById(commentId);
            if (result == null) return null;
            return Mapper.Map(result);
        }

        public IQueryable<int> GetLikedUsers(int commentId)
        {
            throw new NotImplementedException();
        }
    }
}
