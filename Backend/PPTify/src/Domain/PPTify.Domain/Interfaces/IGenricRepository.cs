using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTify.Infrastructure;

namespace PPTify.Domain.Interfaces
{
    public interface IGenricRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> FindByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
