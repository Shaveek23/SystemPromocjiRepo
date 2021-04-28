using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.DTO;

namespace WebApi.Services.Services_Interfaces
{
    public interface IUserService
    {
        public ServiceResult<IQueryable<UserDTO>> GetAll();

        public ServiceResult<UserDTO> GetById(int id);
        public Task<ServiceResult<int?>> AddUserAsync(int userId,UserDTO newUserDTO);
        public Task<ServiceResult<bool>> EditUserAsync(int userId, UserDTO userDTO, int userToBeEditedId);
        public Task<ServiceResult<bool>> DeleteUserAsync(int userId, int userToBeDeletedId);


    }
}
