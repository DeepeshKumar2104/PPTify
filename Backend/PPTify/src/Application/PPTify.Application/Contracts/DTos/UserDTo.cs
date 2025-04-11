using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTify.Application.Contracts.DTos
{
    public class UserDTo
    {
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Role { get; set; } = "user";
        public string? ProfilePictureUrl { get; set; } 
        public string? PhoneNumber { get; set; }
        public bool? IsEmailVerified { get; set; }
        public string Password { get; set; } = string.Empty;


    }
}
