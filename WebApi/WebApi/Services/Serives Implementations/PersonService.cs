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
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public IQueryable<PersonDTO> GetAll()
        { 
            return Mapper.Map(_personRepository.GetAll());
        }

        public PersonDTO GetById(int id)
        {
            return Mapper.Map(_personRepository.GetById(id));
        }

        public async Task<PersonDTO> AddPersonAsync(PersonDTO newPersonDTO)
        {
            Person newPerson = Mapper.Map(newPersonDTO);
            Person createdPerson =  await _personRepository.AddAsync(newPerson);
            return Mapper.Map(createdPerson);
        }


    }
}
