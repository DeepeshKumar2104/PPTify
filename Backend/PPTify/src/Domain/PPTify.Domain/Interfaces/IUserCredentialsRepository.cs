using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTify.Infrastructure;

namespace PPTify.Domain.Interfaces
{
    public interface IUserCredentialsRepository
    {
        Task AddUserCredentials(UserCredentials userCredentials);
        Task<UserCredentials> GetUserCredentials(string email);
        Task<bool> UserExists(Guid userId);
    }
}
