using System;
using System.Collections.Generic;
using System.Text;

namespace PosCore.Models
{
    public class Customer
    {
        public string Id { get; set; } = $"customer:{Guid.NewGuid().ToString()}";
        public bool Sync { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }
        public string DoneBy { get; set; }
        public DateTime Created { get; set; }

    }
}
