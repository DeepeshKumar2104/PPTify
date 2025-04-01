using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTify.Infrastructure;

namespace PPTify.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Users> FindByEmailAsync(string username);   
        Task<Users> FindByIdAsync(Guid id);
        Task<Users> AddAsync(Users user);
        Task<Users> UpdateAsync(Users user);
        Task<Users> DeleteAsync(Users user);
    }
}
