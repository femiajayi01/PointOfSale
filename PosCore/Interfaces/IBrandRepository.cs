using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PosCore.Models;

namespace PosCore.Interfaces
{
    public interface IBrandRepository
    {
        Brand GetBrand(string Id);
        IEnumerable<Brand> GetAllBrands();
        Brand Add(Brand brand);
        Brand Update(Brand brand);
        Brand Delete(string id);
        bool saveChanges();
    }

}

