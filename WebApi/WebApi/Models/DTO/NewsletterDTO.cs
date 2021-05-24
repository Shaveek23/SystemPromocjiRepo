using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers
{
    public class NewsletterDTO
    {
        [Required]
        public int? CategoryID { get; set; }

        [Required]
        public bool? isSubscribed { get; set; }
    }
}
