using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTify.Application.Contracts.DTos;
using PPTify.Application.Contracts.Interface;
using PPTify.Application.Features.Handlers;
using PPTify.Domain.Interfaces;
using PPTify.Infrastructure;

namespace PPTify.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitofWork unitofwork;

        public UserService(IUnitofWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        UserUniqueIdentificationNumber userUniqueIdentificationNumber = new UserUniqueIdentificationNumber();   

        public async Task<bool> RegisterUserAsync(UserDTo userdto)
        {
            await unitofwork.BeginTransactionAsync();
            try
            {
                var user = new Users
                {
                    UserUniqueId = userUniqueIdentificationNumber.UserUniqueIdentifier(userdto.FullName),
                    FullName = userdto.FullName,
                    Email = userdto.Email,
                    Role = userdto.Role,
                    PhoneNumber = userdto.PhoneNumber,
                    ProfilePictureUrl = userdto.ProfilePictureUrl,
                };
                await unitofwork.UserDetailRepository.AddAsync(user);
                var usercred = new UserCredentials
                {
                    UserId = user.UserId,
                    PasswordHash = userdto.PasswordHash,
                };
                await unitofwork.UserCredentialRepository.AddAsync(usercred);
                await unitofwork.SaveChangesAsync();
                await unitofwork.CommitTransactionAsync();  
                return true;

            }
            catch (Exception)
            {
                await unitofwork.RollBackAsync();   
                return false;
            }
        }

        
    }
}
