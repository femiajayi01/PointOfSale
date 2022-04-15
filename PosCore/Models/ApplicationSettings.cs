using System;
using System.Collections.Generic;
using System.Text;

namespace PosCore.Models
{
    public class ApplicationSettings
    {
        public string JwtSecret { get; set; }
        public string ClientUrl { get; set; }
    }
}
