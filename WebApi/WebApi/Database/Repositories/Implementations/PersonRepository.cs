using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Models.POCO;

namespace WebApi.Database.Repositories.Implementations
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(DatabaseContext databaseContext) : base(databaseContext) {}

        public Task<Person> GetPersonByIdAsync(int id)
        {
            return GetAll().FirstOrDefaultAsync(x => x.PersonID == id);
        }
    }
}
