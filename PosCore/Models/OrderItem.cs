using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PosCore.Models
{
    public class OrderItem
    {
        public string Id { get; set; } = $"orderItem:{Guid.NewGuid().ToString()}";
        public bool Sync { get; set; }
        public string ItemNo { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        /*        public int CategoryId { get; set; }
                public Category Category { get; set; }
                public int BrandId { get; set; }
                public Brand Brand { get; set; }*/

        public string Quantity { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal UnitPrice { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalPrice { get; set; }
        public string Created { get; set; }
    


    }
}
