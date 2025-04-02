using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTify.Domain.Interfaces;

namespace PPTify.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<Users> AddAsync(Users user)
        {
            throw new NotImplementedException();
        }

        public Task<Users> DeleteAsync(Users user)
        {
            throw new NotImplementedException();
        }

        public Task<Users> FindByEmailAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<Users> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Users> UpdateAsync(Users user)
        {
            throw new NotImplementedException();
        }
    }
}
