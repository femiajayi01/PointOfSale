using System;
using System.Collections.Generic;
using System.Text;

namespace PosCore.Models
{
    public class Brand
    {
      //  [Required]
        public string Id { get; set; } = $"brand:{Guid.NewGuid().ToString()}";
        public bool Sync { get; set; }
        public string Name { get; set; }
    }
}
