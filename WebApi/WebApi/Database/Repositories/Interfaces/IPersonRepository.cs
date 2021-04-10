using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.POCO;

namespace WebApi.Database.Repositories.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person> GetPersonByIdAsync(int id);
      
    }
}
