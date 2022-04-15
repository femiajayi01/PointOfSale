using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PosCore.Models
{
    public class Product
    {
        public string Id { get; set; } = $"product:{Guid.NewGuid().ToString()}";
        public bool Sync { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal CostPrice { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SellingPrice { get; set; }
        [Required]
        public string Quantity { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [Required]
        public string BrandId { get; set; }
        public Brand Brand { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
