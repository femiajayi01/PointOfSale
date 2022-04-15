using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PosCore.Models
{
    public class Order
    {
        public string Id { get; set; } = $"order:{Guid.NewGuid().ToString()}";
        public bool Sync { get; set; }
        public string OrderNumber { get; set; }
        public string SalesPerson { get; set; }
        public string Quantity { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalAmount { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal AmountPaid { get; set; }
        public string PaymentType { get; set; }
        public string Items { get; set; }
        public bool ReturnStatus { get; set; }
        public string? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string DoneBy { get; set; }
        public DateTime Created { get; set; }
   
    }
}
