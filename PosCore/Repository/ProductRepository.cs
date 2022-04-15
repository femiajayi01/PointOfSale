using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PosCore.Data;
using PosCore.Interfaces;
using PosCore.Models;

namespace PosCore.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;
        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Product Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product Delete(string id)
        {
            Product product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return product;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.Include(x => x.Brand).Include(x => x.Category).ToList();
            //return _context.Products;
        }

        public Product GetProduct(string Id)
        {
            return _context.Products.Find(Id);
        }

        public Product Update(Product productChanges)
        {
            var product = _context.Products.Attach(productChanges);
            product.State  = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return productChanges;
        }


    }
}
