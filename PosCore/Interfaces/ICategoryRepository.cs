using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PosCore.Models;

namespace PosCore.Interfaces
{
    public interface ICategoryRepository
    {
        Category GetCategory(string Id);
        IEnumerable<Category> GetAllCategories();
        Category Add(Category category);
        Category Update(Category category);
        Category Delete(string id);
        bool saveChanges();
    }
}
