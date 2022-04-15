using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using PosCore.Data;
using PosCore.Models;
using PosCore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using PosCore.Interfaces;

namespace PosWeb.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;
        public ProductController(ApplicationContext context, IWebHostEnvironment env, ILogger<ProductController> logger, IProductRepository product)
        {
            _context = context;
            _env     = env;
            _logger  = logger;
            _productRepository = product;
        }
        public IActionResult Index()
        {
            //var products = _context.Products.Include(x => x.Brand).Include(x => x.Category).ToList();
            var products = _productRepository.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model          = new CreateProductViewModel();
            model.CategoryList = _context.Categories.ToList();
            model.BrandList    = _context.Brands.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Images != null && model.Images.Count > 0)
                {
                    foreach (IFormFile image in model.Images)
                    {
                        string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        image.CopyTo(new FileStream(filePath, FileMode.Create));
                    }

                }

                Product newProduct = new Product()
                {
                    Sync         = false,
                    Name         = model.Name,
                    CostPrice    = model.CostPrice,
                    SellingPrice = model.SellingPrice,
                    Quantity     = model.Quantity,
                    Barcode      = model.Barcode,
                    Description  = model.Description,
                    Image        = uniqueFileName,
                    BrandId      = model.BrandId,
                    CategoryId   = model.CategoryId
                };

                _context.Products.Add(newProduct);
                _context.SaveChanges();
                _logger.LogInformation($"New product {newProduct.Name} successfully created");
                return RedirectToAction("index", "Product");
            }
            return View();

        }

        private string ProcessUploadedFile(CreateProductViewModel model)
        {
            string uniqueFileName = null;
            if (model.Images != null && model.Images.Count > 0)
            {
                foreach (IFormFile image in model.Images)
                {
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }

                }
            }

            return uniqueFileName;
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            Product product = _productRepository.GetProduct(id);
            if (product is not null)
            {
                EditProductViewModel editProductViewModel = new EditProductViewModel()
                {
                    Id           = product.Id,
                    Name         = product.Name,
                    CostPrice    = product.CostPrice,
                    SellingPrice = product.SellingPrice,
                    Quantity     = product.Quantity,
                    Barcode      = product.Barcode,
                    Description  = product.Description,
                    BrandId      = product.BrandId,
                    CategoryId   = product.CategoryId,
                    CategoryList = _context.Categories.ToList(),
                    BrandList    = _context.Brands.ToList()
            };
              //  model.CategoryList = _context.Categories.ToList();
              //  model.BrandList = _context.Brands.ToList();
                return View(editProductViewModel);
            }
            return View();
        }



    }
}
