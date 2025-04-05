using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTify.Application.Features.Handlers
{
    public class UserUniqueIdentificationNumber
    {
        public string UserUniqueIdentifier(string fullname)
        {
            string initials = GetInitials(fullname);
            long unixtimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            string firstLetter = char.ToUpper(fullname.Trim()[0]).ToString();
            return $"{firstLetter}-{unixtimestamp}-{initials}";
        }
        public string GetInitials(string name)
        {
            var parts = name.Split(' ',StringSplitOptions.RemoveEmptyEntries);
            return string.Concat(parts.Select(p=>char.ToUpper(p[0])));
        }
    }
}
