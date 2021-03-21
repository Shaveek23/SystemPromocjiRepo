using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models;
using WallProject.Services.Services_Interfaces;

namespace WallProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWallService _service;

        public HomeController(ILogger<HomeController> logger, IWallService service)
        {
            _logger = logger;
           _service = service;
        }

        public async Task<IActionResult> WallAsync()
        {
           
            PersonViewModel user= await _service.getUser();
            WallViewModel wall = new WallViewModel();
            wall.Owner = user;
            //Bedzie uzupełniane jak beda w bazie
            wall.Posts = new List<PostViewModel>();
            return View(wall);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
