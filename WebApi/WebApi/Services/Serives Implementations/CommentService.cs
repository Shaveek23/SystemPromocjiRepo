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
    public class CommentService:ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CommentDTO> AddCommentAsync(CommentDTO comment)
        {
            Comment newComment = Mapper.Map(comment);
            Comment createdComment = await _commentRepository.AddAsync(newComment);
            return Mapper.Map(createdComment);
        }

        public void DeleteComment(int commentId)
        {
            _commentRepository.DeleteComment(commentId);
        }

        public async Task<CommentDTO> EditCommentAsync(CommentDTO comment)
        {
            Comment newComment = Mapper.Map(comment);
            Comment editedComment = await _commentRepository.UpdateAsync(newComment);
            return Mapper.Map(editedComment);
        }

        public Task EditLikeOnCommentAsync(int commentId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CommentDTO> GetAll()
        {
            return Mapper.Map(_commentRepository.GetAll());
        }

        public CommentDTO GetById(int commentId)
        {
            return Mapper.Map(_commentRepository.GetById(commentId));
        }

        public IQueryable<int> GetLikedUsers(int commentId)
        {
            throw new NotImplementedException();
        }
    }
}
