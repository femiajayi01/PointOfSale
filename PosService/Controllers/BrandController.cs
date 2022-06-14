using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PosCore;
using PosCore.Interfaces;
using PosCore.Models;
using PosCore.ViewModels;


namespace PosService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IRepository<Brand> _repository;

        public BrandController(IBrandRepository brand, IRepository<Brand> repository)
        {
            _brandRepository = brand;
            _repository = repository;
        }

        [HttpGet]
        [Route("GetAsync")]
        public async Task<IEnumerable<Brand>> GetAsync()
        {
            var brands = await _repository.GetAllAsync();
            return brands;
        }

        [HttpGet]
        [Route("GetAsyncId/{id}")]
        public async Task<ActionResult> GetAsyncId(string id)
        {
            var brand = await _repository.GetByIdAsync(id);
            if (brand is null) return NotFound();
            return Ok(brand);
        }

        // POST: api/Brand/Create
        [HttpPost]
        [Route("CreateAsync")]
        public async Task<ActionResult> CreateAsync(CreateBrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                Brand newBrand = new Brand()
                {
                    Name = model.Name,
                    Sync = false
                };
                await _repository.CreateAsync(newBrand);
                return Ok(newBrand);
            }
            return BadRequest();
        }

        [HttpPut("UpdateAsync/{id}")]
       // [Route("UpdateAsync/{id}")]
        public async Task<IActionResult> UpdateAsync(string id, EditBrandViewModel model)
        {
            var existingBrand = await _repository.GetByIdAsync(id);
            if (existingBrand is null) return NotFound();

            existingBrand.Name = model.Name;
            await _repository.UpdateAsync(existingBrand);

            return NoContent();
        }

       [HttpDelete("DeleteAsync/{id}")]
       public async Task<IActionResult> DeleteAsync(string id)
       {
           var brand = await _repository.GetByIdAsync(id);
           if (brand is null) return NotFound();

           await _repository.DeleteAsync(brand);
           return NoContent();
       }


        //...................................................................................................

        // GET: api/Index
        [HttpGet]
       // [Authorize]
        public BasicResponse Index()
        {
            var brands =  _brandRepository.GetAllBrands(); // _context.Brands.ToListAsync();
            return BasicResponse.SuccessResponse("Success", brands);
        }



        // GET: api/Brand/GetBrandById
        [HttpGet]
        [Route("GetBrandById/{id}")]
        public BasicResponse GetBrand(string id)
        {
            var response = BasicResponse.FailureResponse($"Brand with Brand Id {id} not found");

            Brand brand =  _brandRepository.GetBrand(id);
            if (brand is not null)
                return BasicResponse.SuccessResponse("success", brand);
            return response;
        }

        // POST: api/Brand/Create
        [HttpPost]
        [Route("Create")]
        public BasicResponse Create(CreateBrandViewModel model)
        {
            var response = BasicResponse.FailureResponse("failed");
            if (ModelState.IsValid)
            {
                Brand newBrand = new Brand()
                {
                    Name = model.Name,
                    Sync = false
                };
                _brandRepository.Add(newBrand);
                return BasicResponse.SuccessResponse("success", newBrand);
            }

            return response;
        }






    }
}
