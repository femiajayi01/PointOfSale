using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PosCore.Data;
using PosCore.Interfaces;
using PosCore.Models;
using PosCore.ViewModels;

namespace PosWeb.Controllers
{
    [Authorize]
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brand)
        {
            _brandRepository = brand;
        }

        public IActionResult Index()
        {
            //var model = _context.Brands .ToList();
            var model = _brandRepository.GetAllBrands();
            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateBrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                Brand newBrand = new Brand()
                {
                    Name = model.Name,
                    Sync = false
                };
                _brandRepository.Add(newBrand);
                return RedirectToAction("index", "Brand");
            }
            return View();

        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            Brand brand = _brandRepository.GetBrand(id);
            if (brand is not null)
            {
                EditBrandViewModel editBrandViewModel = new EditBrandViewModel()
                {
                    Id = brand.Id,
                    Name = brand.Name
                };
                return View(editBrandViewModel);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Edit(EditBrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                Brand brand = _brandRepository.GetBrand(model.Id);
                brand.Name = model.Name;
                _brandRepository.Update(brand);
                return RedirectToAction("index");
            }
            return View();

        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            Brand brand = _brandRepository.GetBrand(id);
            if (brand is not null)
            {
                EditBrandViewModel editBrandViewModel = new EditBrandViewModel()
                {
                    Id = brand.Id,
                    Name = brand.Name
                };
                return View(editBrandViewModel);
            }

            return View();
        }

        [HttpPost]
        public  IActionResult DeleteBrand(string id)
        {
            Brand brand =  _brandRepository.GetBrand(id);
            if (brand is null)
            {
                ViewBag.ErrorMessage = $"Brand with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                _brandRepository.Delete(id);
                return RedirectToAction("index");
            }
        }




    }
}
