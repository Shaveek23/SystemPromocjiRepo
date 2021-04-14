using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.DTO;

namespace WebApi.Services.Services_Interfaces
{
    public interface IPersonService
    {
        public ServiceResult<IQueryable<PersonDTO>> GetAll();
        
        public ServiceResult<PersonDTO> GetById(int id);
        public Task<ServiceResult<int?>> AddPersonAsync(PersonDTO newPersonDTO);
    }
}
