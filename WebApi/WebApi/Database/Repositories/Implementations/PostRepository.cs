using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Exceptions;
using WebApi.Models.DTO.PostDTOs;
using WebApi.Models.POCO;

namespace WebApi.Database.Repositories.Implementations
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DatabaseContext databaseContext) : base(databaseContext) { }

        #region TO DO :  zintegorować z generyczną metodą UpdateAsync

        public Task<Post> EditPostAsync(int id, PostEditDTO body)
        {
            var postToEdit = dbContext.Posts.SingleOrDefault(post => post.PostID == id);
           
            if (postToEdit == null)
            {
                throw new EditPostFailException($"{nameof(AddAsync)} there is no post with given post ID.");
            }
            else
            {
                postToEdit.Title = body.title;
                postToEdit.Content = body.content;
                postToEdit.CategoryID = body.category.Value;
                postToEdit.Date = body.dateTime.Value;
                postToEdit.IsPromoted = body.isPromoted.Value;
            }
            return UpdateAsync(postToEdit);
        }

        #endregion
    }
}
