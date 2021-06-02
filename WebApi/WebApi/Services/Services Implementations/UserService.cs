using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Mapper;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Models.DTO;
using WebApi.Models.DTO.UserDTOs;
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

        public ServiceResult<IQueryable<UserGetDTO>> GetAll()
        {
            var result = _userRepository.GetAll();
            return new ServiceResult<IQueryable<UserGetDTO>>(UserMapper.MapGet(result.Result), result.Code, result.Message);
        }

        public ServiceResult<UserGetDTO> GetById(int id)
        {
            var result = _userRepository.GetById(id);
            return new ServiceResult<UserGetDTO>(UserMapper.MapGet(result.Result), result.Code, result.Message);
        }

        public async Task<ServiceResult<idDTO>> AddUserAsync(int userId, UserPostDTO newUserDTO)
        {
            User newUser = UserMapper.Map(newUserDTO);
            newUser.Timestamp = DateTime.Now;
            var result = await _userRepository.AddAsync(newUser);
            return new ServiceResult<idDTO>(Mapper.Map(result.Result?.UserID), result.Code, result.Message);
        }

        public async Task<ServiceResult<bool>> EditUserAsync(int userID, UserPutDTO userDTO, int userToBeEditedId)
        {
            User user = UserMapper.Map(userDTO);
            user.UserID = userToBeEditedId;
            var result = await _userRepository.UpdateAsync(user, userID);
            return new ServiceResult<bool>(result.IsOk(), result.Code, result.Message);
        }

        public async Task<ServiceResult<bool>> DeleteUserAsync(int userID, int userToBeDeletedId)
        {
            var GetResult = _userRepository.GetById(userToBeDeletedId);

            if (!GetResult.IsOk())
                return new ServiceResult<bool>(false, GetResult.Code, GetResult.Message);

            var RemoveResult = await _userRepository.RemoveAsync(GetResult.Result, userID);
            return new ServiceResult<bool>(RemoveResult.IsOk(), RemoveResult.Code, RemoveResult.Message);
        }
    }
}