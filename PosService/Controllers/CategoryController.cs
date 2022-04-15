using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosCore.Interfaces;
using PosCore.Models;
using PosCore.ViewModels;

namespace PosService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        // GET: api/Index
        [HttpGet]
        public BasicResponse Index()
        {
            var categories = _categoryRepository.GetAllCategories(); 
            return BasicResponse.SuccessResponse("Success", categories);
        }

        // GET: api/Brand/GetCategoryById
        [HttpGet]
        [Route("GetCategoryById")]
        public BasicResponse GetCategory(string id)
        {
            var response = BasicResponse.FailureResponse($"Category with Category Id {id} not found");

            Category category = _categoryRepository.GetCategory(id);
            if (category is not null)
                return BasicResponse.SuccessResponse("success", category);
            return response;
        }

        // POST: api/Category/Create
        [HttpPost]
        [Route("Create")]
        public BasicResponse Create(CreateCategoryViewModel model)
        {
            var response = BasicResponse.FailureResponse("failed");
            if (ModelState.IsValid)
            {
                Category newCategory = new Category()
                {
                    Name = model.Name,
                    Sync = false
                };
                _categoryRepository.Add(newCategory);
                return BasicResponse.SuccessResponse("success", newCategory);
            }
            return response;
        }


    }
}
