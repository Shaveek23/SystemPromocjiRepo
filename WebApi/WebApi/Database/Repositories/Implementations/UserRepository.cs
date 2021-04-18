using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Models.POCO;
using WebApi.Services;

namespace WebApi.Database.Repositories.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext databaseContext) : base(databaseContext) { }

    }
}
