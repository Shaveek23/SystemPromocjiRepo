using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.DTO;
using WebApi.Services;
using WebApi.Services.Services_Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    public class CategoryController: ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet("categories")]
        public ActionResult<IQueryable<CategoryDTO>> GetAll()
        {
            var result = _categoryService.GetAll();
            return new ControllerResult<IQueryable<CategoryDTO>>(result).GetResponse();
        }
        [HttpGet("categories/{categoryId}")]
        public ActionResult<IQueryable<CategoryDTO>> GetById(int categoryId)
        {
            var result = _categoryService.GetById(categoryId);
            return new ControllerResult<CategoryDTO>(result).GetResponse();
        }
    }
}
