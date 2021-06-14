using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WallProject.Models.DTO.UserDTOs
{
    public class UserPutDTO
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsEntrepreneur { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
    }
}
