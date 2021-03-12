using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.DTO;

namespace WebApi.Services.Services_Interfaces
{
    public interface IPersonService
    {
        public IQueryable<PersonDTO> GetAll();
        
        public PersonDTO GetById(int id);
        public Task<PersonDTO> AddPersonAsync(PersonDTO newPersonDTO);
    }
}
