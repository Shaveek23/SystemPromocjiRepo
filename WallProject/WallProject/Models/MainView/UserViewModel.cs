using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WallProject.Models.MainView
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public System.DateTime Timestamp { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsEnterprenuer { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
    }
}
