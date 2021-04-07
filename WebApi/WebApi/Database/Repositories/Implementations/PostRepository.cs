using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Models.DTO.PostDTOs;
using WebApi.Models.POCO;

namespace WebApi.Database.Repositories.Implementations
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DatabaseContext databaseContext) : base(databaseContext) { }

        public Task<Post> GetPostByIdAsync(int id)
        {
            return GetAll().FirstOrDefaultAsync(x => x.PostID == id);
        }


        public Task<Post> EditPostAsync(int id, PostEditDTO body)
        {
            var postToEdit = dbContext.Posts.SingleOrDefault(post => post.PostID == id);
            //TODO:
            //Zmienić zwracany wyjątek na jeden z tych zaimplementowanych przez golika.
            if (postToEdit == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} there is no post with given post ID.");
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
    }
}
