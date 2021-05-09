using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WallProject.Models.DTO
{
    public class UserDTO
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public System.DateTime timestamp { get; set; }
        public bool isAdmin { get; set; }
        public bool isEnterprenuer { get; set; }
        public bool isVerified { get; set; }
        public bool isActive { get; set; }
    }
}
