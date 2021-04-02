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

        public async Task<CommentDTO> AddCommentAsync(int userId,CommentDTO comment)
        {
            //Tu nw bo odziedziczona
            Comment newComment = Mapper.Map(comment);
            Comment createdComment = await _commentRepository.AddAsync(newComment);
            if (createdComment == null) return null;
            return Mapper.Map(createdComment);

        }

        public void DeleteComment(int commentId,int userId)
        {
            _commentRepository.DeleteComment(commentId,userId);
        }

        public async Task<CommentDTO> EditCommentAsync(int commentId,int userId,CommentDTO comment)
        {
            Comment newComment = Mapper.Map(comment);
            Comment editedComment = await _commentRepository.UpdateAsync(newComment);
            return Mapper.Map(editedComment);
        }

        public Task EditLikeOnCommentAsync(int commentId,int userId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CommentDTO> GetAll(int userId)
        {
            return Mapper.Map(_commentRepository.GetAll());
        }

        public CommentDTO GetById(int commentId,int userId)
        {
            return Mapper.Map(_commentRepository.GetById(commentId));
        }

        public IQueryable<int> GetLikedUsers(int commentId)
        {
            throw new NotImplementedException();
        }
    }
}
