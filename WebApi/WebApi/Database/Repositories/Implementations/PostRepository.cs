using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Repositories.Interfaces;
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
    }
}
