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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ServiceResult<IQueryable<UserDTO>> GetAll()
        {
            var result = _userRepository.GetAll();
            return new ServiceResult<IQueryable<UserDTO>>(Mapper.Map(result.Result), result.Code, result.Message);
        }

        public ServiceResult<UserDTO> GetById(int id)
        {
            var result = _userRepository.GetById(id);
            return new ServiceResult<UserDTO>(Mapper.Map(result.Result), result.Code, result.Message);
        }

        public async Task<ServiceResult<int?>> AddUserAsync(int userId,UserDTO newUserDTO)
        {
            User newUser = Mapper.Map(newUserDTO);
            
            var result = await _userRepository.AddAsync(newUser);
            return new ServiceResult<int?>(result.Result?.UserID, result.Code, result.Message);
        }

        public async Task<ServiceResult<bool>> EditUserAsync(int userId, UserDTO userDTO, int userToBeEditedId)
        {
            User user = Mapper.Map(userDTO);
            user.UserID = userToBeEditedId;
            var result = await _userRepository.UpdateAsync(user);
            return new ServiceResult<bool>(result.IsOk(), result.Code, result.Message);
        }

        public async Task<ServiceResult<bool>> DeleteUserAsync(int userId, int userToBeDeletedId)
        {
            var GetResult = _userRepository.GetById(userToBeDeletedId);

            if (!GetResult.IsOk())
                return new ServiceResult<bool>(false, GetResult.Code, GetResult.Message);

            var RemoveResult = await _userRepository.RemoveAsync(GetResult.Result);
            return new ServiceResult<bool>(RemoveResult.IsOk(), RemoveResult.Code, RemoveResult.Message);
        }
    }
}
