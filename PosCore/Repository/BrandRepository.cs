using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Threading.Tasks;
using PosCore.Data;
using PosCore.Interfaces;
using PosCore.Models;
using StackExchange.Redis;

namespace PosCore.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationContext _context;
        //private readonly IRedisRepository _redis;

        public BrandRepository(ApplicationContext context/*, IRedisRepository redis*/)
        {
            _context   = context;
           // _redis     = redis;
        }

        public Brand Add(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();

            //_redis.SetRecord(brand.Id.ToString(), brand);

            return brand;
        }

        public Brand Delete(string id)
        {
            Brand brand = _context.Brands.Find(id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
                _context.SaveChanges();
            }
            return brand;
        }

        public  IEnumerable<Brand> GetAllBrands()
        {
            return  _context.Brands;
        }

       /* public IEnumerable<Brand> GetAllBrands()
        {
            var record = _redis.GetAllRecords<Brand>();
            if (record is not null)
                return record;

            return _context.Brands;
        }*/

        public Brand GetBrand(string id)
        {
            var brand =  _context.Brands.Find(id);
            return brand;
        }

       /* public Brand GetBrand(string id)
        {
            var record = _redis.GetRecord(id.ToString());
            if (!record.IsNull)
                return JsonSerializer.Deserialize<Brand>(record);

            var brand = _context.Brands.Find(id);
            if (brand is not null)
                _redis.SetRecord(brand.Id.ToString(), brand);
            return brand;
        }*/

        public Brand Update(Brand brandChanges)
        {
            var brand = _context.Brands.Attach(brandChanges);
            brand.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return brandChanges;
        }

        public bool saveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }



    }
}
