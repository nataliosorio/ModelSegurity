using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Entity.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task<T> CreateAsync(T entity) { _dbSet.Add(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task<bool> UpdateAsync(T entity) { _context.Entry(entity).State = EntityState.Modified; await _context.SaveChangesAsync(); return true; }
        public async Task<bool> DeleteAsync(int id) { var entity = await _dbSet.FindAsync(id); if (entity == null) return false; _dbSet.Remove(entity); await _context.SaveChangesAsync(); return true; }
        public async Task<bool> DeleteLogicalAsync(int id) { var entity = await _dbSet.FindAsync(id); if (entity == null) return false; entity.GetType().GetProperty("IsDeleted")?.SetValue(entity, true); _context.Entry(entity).State = EntityState.Modified; await _context.SaveChangesAsync(); return true; }

    }
}
