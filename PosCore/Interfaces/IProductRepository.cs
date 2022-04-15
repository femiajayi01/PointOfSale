using System;
using System.Collections.Generic;
using System.Text;
using PosCore.Models;

namespace PosCore.Interfaces
{
    public interface IProductRepository
    {
        Product GetProduct(string Id);
        IEnumerable<Product> GetAllProducts();
        Product Add(Product product);
        Product Update(Product product);
        Product Delete(string id);
    }
}
