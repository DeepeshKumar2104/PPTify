using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTify.Application.Contracts.DTos
{
    public class UserCredentialDto
    {
        public Guid UserId { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public string? TwoFactorSecret { get; set; }

    }
}
