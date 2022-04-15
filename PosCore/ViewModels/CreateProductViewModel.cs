using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Http;
using PosCore.Models;

namespace PosCore.ViewModels
{
    public class CreateProductViewModel
    {

        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Required]
        [Display(Name = "Cost Price")]
        public decimal CostPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Required]
        [Display(Name = "Selling Price")]
        public decimal SellingPrice { get; set; }
        [Required]
        [Display(Name = "Stock")]
        public string Quantity { get; set; }
        [Required]
        public string Barcode { get; set; }
        [Display(Name = "Product Description")]
        public string Description { get; set; }
        [Display(Name = "Product Image")]
       /* public string Image { get; set; }*/
        public List<IFormFile> Images { get; set; }
        public Brand Brand { get; set; }
        [Required]
        public string BrandId { get; set; }
        [Required]
        public List<Brand> BrandList{ get; set; }
        public Category Category { get; set; }
        public string CategoryId { get; set; }
        public List<Category> CategoryList { get; set; }

    }
}
