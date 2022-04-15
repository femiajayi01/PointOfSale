using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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


        public BrandController(IBrandRepository brand)
        {
            _brandRepository = brand;
        }


        // GET: api/Index
        [HttpGet]
        public BasicResponse Index()
        {
            var brands =  _brandRepository.GetAllBrands(); // _context.Brands.ToListAsync();
            return BasicResponse.SuccessResponse("Success", brands);
        }



        // GET: api/Brand/GetBrandById
        [HttpGet]
        [Route("GetBrandById")]
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
