using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using PPTify.Domain.Interfaces;
using PPTify.Infrastructure.Persistence.DbContexts;

namespace PPTify.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitofWork
    {
        private readonly AppDbContext context;
        private IDbContextTransaction transaction;
        public IGenricRepository<Users> UserDetailRepository { get; }

        public IGenricRepository<UserCredentials> UserCredentialRepository { get; }

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
            UserDetailRepository = new GenericRepository<Users>(context);
            UserCredentialRepository = new GenericRepository<UserCredentials>(context);
        }
        public async Task BeginTransactionAsync()
        {
           transaction = await context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await transaction.CommitAsync();
        }

        public async Task RollBackAsync()
        {
            await transaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
