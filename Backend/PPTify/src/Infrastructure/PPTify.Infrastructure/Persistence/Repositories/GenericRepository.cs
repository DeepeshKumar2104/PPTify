using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PPTify.Domain.Interfaces;
using PPTify.Infrastructure.Persistence.DbContexts;

namespace PPTify.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T>:IGenricRepository<T> where T : class
    {
        private readonly AppDbContext context;
        private readonly DbSet<T> dbSet;
        public GenericRepository(AppDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
           await dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<T?> FindByIdAsync(Guid id)
        {
            var result = await dbSet.FindAsync(id);
            return result;
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return await dbSet.ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await Task.CompletedTask;
        }
    }
}
