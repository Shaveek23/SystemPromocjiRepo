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


        public async Task<CommentDTOOutput> AddCommentAsync(int userId, CommentDTO comment)
        {

            Comment newComment = Mapper.Map(comment);
            Comment createdComment = await _commentRepository.AddAsync(newComment);
            if (createdComment == null) return null;
            return Mapper.MapOutput(createdComment);

        }

        public bool DeleteComment(int commentId, int userId)
        {
            return _commentRepository.Delete(commentId, userId);
        }

        public async Task<CommentDTOOutput> EditCommentAsync(int commentId, int userId, CommentDTO comment)
        {
            Comment newComment = Mapper.Map(comment);
            newComment.CommentID = commentId;
            Comment editedComment = await _commentRepository.UpdateAsync(newComment);
            if (editedComment == null) return null;
            return Mapper.MapOutput(editedComment);
        }

        public Task<bool> EditLikeOnCommentAsync(int commentId, int userId)

        {
            throw new NotImplementedException();
        }

        public IQueryable<CommentDTOOutput> GetAll(int userId)
        {
            var result = _commentRepository.GetAll();
            if (result == null) return null;
            return Mapper.MapOutput(result);
        }

        public CommentDTOOutput GetById(int commentId, int userId)
        {

            var result = _commentRepository.GetById(commentId);
            if (result == null) return null;
            return Mapper.MapOutput(result);

        }

        public IQueryable<int> GetLikedUsers(int commentId)
        {
            throw new NotImplementedException();
        }
    }
}
