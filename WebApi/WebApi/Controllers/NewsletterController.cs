using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Services.Services_Implementations;

namespace WebApi.Controllers
{
    [Route("newsletter")]
    [ApiController]
    public class NewsletterController : ControllerBase
    {
        private readonly INewsletterService _newsletterService;
        private readonly ILogger<NewsletterController> _logger;

        public NewsletterController(INewsletterService newsletterService, ILogger<NewsletterController> logger)
        {
            _logger = logger;
            _newsletterService = newsletterService;
        }

        [HttpPost]
        public async Task<IActionResult> SetSubscribe([Required][FromHeader] int userID, [Required][FromBody] NewsletterDTO body)
        {
            var res = await _newsletterService.SetSubscriptionAsync(body, userID, body.isSubscribed.Value);
            return new ControllerResult<bool>(res).GetResponse();
        }
    }
}
