using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.DTO;
using WebApi.Models.DTO.UserDTOs;

namespace WebApi.Services.Services_Interfaces
{
    public interface IUserService
    {
        public ServiceResult<IQueryable<UserGetDTO>> GetAll();

        public ServiceResult<UserGetDTO> GetById(int id);
        public Task<ServiceResult<int?>> AddUserAsync(int userId, UserPostDTO newUserDTO);
        public Task<ServiceResult<bool>> EditUserAsync(int userId, UserPutDTO userDTO, int userToBeEditedId);
        public Task<ServiceResult<bool>> DeleteUserAsync(int userId, int userToBeDeletedId);


    }
}
