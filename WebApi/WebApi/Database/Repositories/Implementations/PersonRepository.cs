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
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(DatabaseContext databaseContext) : base(databaseContext) {}

        public async Task<ServiceResult<Person>> GetPersonByIdAsync(int id)
        {
            return new ServiceResult<Person>(await GetAll().Result.FirstOrDefaultAsync(x => x.PersonID == id));
        }
    }
}
