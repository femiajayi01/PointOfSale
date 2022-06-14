using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PosCore.Data;
using PosCore.Models;

namespace PosCore
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext _context;

        public Repository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            return  await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            var model =  _context.Set<T>().Attach(entity);
            model.State = EntityState.Modified;
            await _context.SaveChangesAsync();
           // return entity;
        }

/*        public async Task<T> UpdateAsync(T entity)
        {
            var model = _context.Set<T>().Attach(entity);
            model.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }*/


        public bool saveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
