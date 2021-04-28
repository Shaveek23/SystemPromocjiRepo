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

        public ServiceResult<IQueryable<PersonDTO>> GetAll()
        {
            var result = _personRepository.GetAll();
            return new ServiceResult<IQueryable<PersonDTO>>(Mapper.Map(result.Result), result.Code, result.Message);
        }

        public ServiceResult<PersonDTO> GetById(int id)
        {
            var result = _personRepository.GetById(id);
            return new ServiceResult<PersonDTO>(Mapper.Map(result.Result), result.Code, result.Message);
        }

        public async Task<ServiceResult<int?>> AddPersonAsync(PersonDTO newPersonDTO)
        {
            Person newPerson = Mapper.Map(newPersonDTO);
            var result =  await _personRepository.AddAsync(newPerson);
            return new ServiceResult<int?>(result.Result?.PersonID, result.Code, result.Message);
        }

       
    }
}
