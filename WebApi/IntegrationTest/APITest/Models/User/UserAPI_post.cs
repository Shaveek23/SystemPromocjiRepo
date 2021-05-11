using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationTest.APITest.Models.User
{
    public class UserAPI_post
    {
        public string userName;
        public string userEmail;
        public DateTime? timestamp;
        public bool? isAdmin;
        public bool? isEntrepreneur;
        public bool? isVerified;
        public bool? isActive;
    }
}
