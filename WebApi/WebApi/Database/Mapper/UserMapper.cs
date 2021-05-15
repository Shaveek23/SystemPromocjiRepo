using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.DTO.UserDTOs;
using WebApi.Models.POCO;

namespace WebApi.Database.Mapper
{
    public class UserMapper
    {
        public static UserGetDTO MapGet(User user)
        {
            if (user == null)
                return null;
            return new UserGetDTO
            {
                id= user.UserID,
                userName = user.UserName,
                userEmail = user.UserEmail,
                isVerified = user.IsVerified,
                isAdmin = user.IsAdmin,
                isEntrepreneur = user.IsEnterprenuer,
                isActive = user.Active
            };
        }
        public static User Map(UserGetDTO userDTO)
        {
            if (userDTO == null)
                return null;
            return new User
            {
                UserID = userDTO.id ,
                UserName = userDTO.userName,
                UserEmail = userDTO.userEmail,            
                IsVerified = userDTO.isVerified,
                IsAdmin = userDTO.isAdmin,
                IsEnterprenuer = userDTO.isEntrepreneur,
                Active = userDTO.isActive
            };
        }
        public static IQueryable<UserGetDTO> MapGet(IQueryable<User> people)
        {
            if (people == null)
                return null;
            List<UserGetDTO> list = new List<UserGetDTO>();
            foreach (var person in people)
            {
                list.Add(MapGet(person));
            }

            return list.AsQueryable();
        }


        public static IQueryable<User> Map(IQueryable<UserGetDTO> people)
        {
            if (people == null)
                return null;
            List<User> list = new List<User>();
            foreach (var person in people)
            {
                list.Add(Map(person));
            }

            return list.AsQueryable();
        }
        public static UserPostDTO MapPost(User user)
        {
            if (user == null)
                return null;
            return new UserPostDTO
            {
                
                userName = user.UserName,
                userEmail = user.UserEmail,
                isVerified = user.IsVerified,
                isAdmin = user.IsAdmin,
                isEntrepreneur = user.IsEnterprenuer,
                isActive = user.Active
            };
        }
        public static User Map(UserPostDTO userDTO)
        {
            if (userDTO == null)
                return null;
            return new User
            {
               
                UserName = userDTO.userName,
                UserEmail = userDTO.userEmail,
                IsVerified = userDTO.isVerified.Value,
                IsAdmin = userDTO.isAdmin.Value,
                IsEnterprenuer = userDTO.isEntrepreneur.Value,
                Active = userDTO.isActive.Value
            };
        }
        public static IQueryable<UserPostDTO> MapPost(IQueryable<User> people)
        {
            if (people == null)
                return null;
            List<UserPostDTO> list = new List<UserPostDTO>();
            foreach (var person in people)
            {
                list.Add(MapPost(person));
            }

            return list.AsQueryable();
        }


        public static IQueryable<User> Map(IQueryable<UserPostDTO> people)
        {
            if (people == null)
                return null;
            List<User> list = new List<User>();
            foreach (var person in people)
            {
                list.Add(Map(person));
            }

            return list.AsQueryable();
        }
        public static UserPutDTO MapPut(User user)
        {
            if (user == null)
                return null;
            return new UserPutDTO
            {
                
                userName = user.UserName,
                userEmail = user.UserEmail,
                isVerified = user.IsVerified,
                isAdmin = user.IsAdmin,
                isEntrepreneur = user.IsEnterprenuer,
                isActive = user.Active
            };
        }
        public static User Map(UserPutDTO userDTO)
        {
            if (userDTO == null)
                return null;
            return new User
            {

                UserName = userDTO.userName,
                UserEmail = userDTO.userEmail,
                IsVerified = userDTO.isVerified.Value,
                IsAdmin = userDTO.isAdmin.Value,
                IsEnterprenuer = userDTO.isEntrepreneur.Value,
                Active = userDTO.isActive.Value
            };
        }
        public static IQueryable<UserPutDTO> MapPut(IQueryable<User> people)
        {
            if (people == null)
                return null;
            List<UserPutDTO> list = new List<UserPutDTO>();
            foreach (var person in people)
            {
                list.Add(MapPut(person));
            }

            return list.AsQueryable();
        }


        public static IQueryable<User> Map(IQueryable<UserPutDTO> people)
        {
            if (people == null)
                return null;
            List<User> list = new List<User>();
            foreach (var person in people)
            {
                list.Add(Map(person));
            }

            return list.AsQueryable();
        }

    }
}
