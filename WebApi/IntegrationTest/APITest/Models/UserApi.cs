using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationTest.APITest.Models
{
    public class UserApi
    {
        public int? ID { get; set; }
        public string? UserName { get; set; }
        public string UserEmail { get; set; }
        public System.DateTime? Timestamp { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsEntrepreneur { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsActive { get; set; }
    }
}
