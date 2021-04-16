using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;


namespace WebApi.Models.POCO
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public string UserName { get; set; }
        
        [EmailAddress]
        public string UserEmail { get; set; }

        [Timestamp]
        public System.DateTime Timestamp { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsEnterprenuer { get; set; }
        public bool IsVerified { get; set; }
        public bool Active { get; set; }
    }
}
