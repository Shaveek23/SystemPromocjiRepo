using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.POCO
{
    public class User : IUserable
    {
        [Key]
        public int UserID { get; set; }

        public string UserName { get; set; }
        
        [EmailAddress]
        public string UserEmail { get; set; }

        public System.DateTime Timestamp { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsEnterprenuer { get; set; }
        public bool IsVerified { get; set; }
        public bool Active { get; set; }
        public int GetOwner() => UserID;
    }
}
