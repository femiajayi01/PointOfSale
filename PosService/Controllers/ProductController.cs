using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosCore.Interfaces;
using PosCore.Models;

namespace PosService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Index
        [HttpGet]
        public IEnumerable<Product> Index()
        {
            var result = _productRepository.GetAllProducts(); // _context.Brands.ToListAsync();
            return result;
        }




    }
}
