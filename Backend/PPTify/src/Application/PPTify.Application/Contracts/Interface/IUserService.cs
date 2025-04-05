using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTify.Application.Contracts.DTos;

namespace PPTify.Application.Contracts.Interface
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserDTo userdto);
    }
}
