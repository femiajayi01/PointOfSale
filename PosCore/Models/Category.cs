using System;
using System.Collections.Generic;
using System.Text;

namespace PosCore.Models
{
    public class Category
    {
        public string Id { get; set; } = $"category:{Guid.NewGuid().ToString()}";
        public bool Sync { get; set; }
        public string Name { get; set; }
    }
}
