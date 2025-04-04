using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTify.Infrastructure;

namespace PPTify.Domain.Interfaces
{
    public interface IUnitofWork
    {
        IGenricRepository<Users> UserDetailRepository { get; }
        IGenricRepository<UserCredentials> UserCredentialRepository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollBackAsync();
    }
}
