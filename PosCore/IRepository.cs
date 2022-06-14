using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PosCore
{
    public interface IRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<T> GetByIdAsync(string id);
        Task<T> GetAsync(Guid id);
        Task CreateAsync(T entity);
      //  Task<T> UpdateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        bool saveChanges();
    }


}
