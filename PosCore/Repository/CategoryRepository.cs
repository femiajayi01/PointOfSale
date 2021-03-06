using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PosCore.Data;
using PosCore.Interfaces;
using PosCore.Models;

namespace PosCore.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationContext _context;
        //private readonly IRedisRepository _redis;

        public CategoryRepository(ApplicationContext context/*, IRedisRepository redis*/)
        {
            _context = context;
            //_redis = redis;
        }
        public Category Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            //_redis.SetRecord(category.Id.ToString(), category);

            return category;
        }

        public Category Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }

        /*public IEnumerable<Category> GetAllCategories()
        {
            var record = _redis.GetAllRecords<Category>();
            if (record is not null)
                return record;

            return _context.Categories;
        }*/

        public Category GetCategory(string id)
        {
            var category = _context.Categories.Find(id);
            return category;
        }

        /*public Category GetCategory(string id)
        {
            var record = _redis.GetRecord(id.ToString());
            if (!record.IsNull)
                return JsonSerializer.Deserialize<Category>(record);

            var category = _context.Categories.Find(id);
            if (category is not null)
                _redis.SetRecord(category.Id.ToString(), category);
            return category;
        }*/

        public bool saveChanges()
        {
            throw new NotImplementedException();
        }

        public Category Update(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
