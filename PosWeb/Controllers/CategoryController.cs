using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PosCore.Data;
using PosCore.Models;
using PosCore.ViewModels;
using Microsoft.Extensions.Logging;

namespace PosWeb.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {

        private readonly ApplicationContext _context;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ApplicationContext context, ILogger<CategoryController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var model = _context.Categories.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category newCategory = new Category()
                {
                    Name = model.Name,
                    Sync = false
                };

                _context.Categories.Add(newCategory);
                _context.SaveChanges();
                _logger.LogInformation($"New category {newCategory.Name} successfully created");
                return RedirectToAction("index", "Category");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category is not null)
            {
                EditCategoryViewModel editCategoryViewModel = new EditCategoryViewModel()
                {
                    Id = category.Id,
                    Name = category.Name
                };
                return View(editCategoryViewModel);
            }
            return View();
        }



    }
}
