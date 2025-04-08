using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTify.Application.Contracts.DTos;
using PPTify.Application.Contracts.Interface;
using PPTify.Application.Features.Handlers;
using PPTify.Application.Models.RequestModels;
using PPTify.Application.Models.ResponseModels;
using PPTify.Domain.Interfaces;
using PPTify.Infrastructure;
using BCrypt.Net;
using System.Reflection.Metadata.Ecma335;

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
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(userdto.PasswordHash),
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
        public async Task<ResponseModels> LoginUserAsync(LoginRequestModels loginrequest)
        {
            var user = (await unitofwork.UserDetailRepository.GetAllAsync())
                .FirstOrDefault(u => u.Email == loginrequest.Email);

            var credentials = (await unitofwork.UserCredentialRepository.GetAllAsync())
                .FirstOrDefault(cred => cred.UserId == user.UserId);

            if(user is null || credentials is null)
            {
                return new ResponseModels 
                { 
                    Token = null,
                    Message = "User details not found",
                    Email = loginrequest.Email,
                };
            }

            if(credentials.PasswordHash != loginrequest.Password)
            {
                return new ResponseModels 
                {
                    Message = $"Password is incorrect with the associated {loginrequest.Email}",
                    Email = loginrequest.Email,
                };
            }
            //var tokken = GenerateTokken(user);
            return new ResponseModels 
            { 
                Message= "Login Successfully",
                Token = "Deepesh",
                FullName = user.FullName,
                Email = loginrequest.Email,
            };

        }

    }

}
