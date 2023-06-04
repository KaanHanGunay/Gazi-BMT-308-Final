using System;
using Microsoft.AspNetCore.Identity;

namespace Gazi_BMT_308_Final.Models
{
    public class User : IdentityUser<int>
    {
        public ICollection<UserBook>? UserBooks { get; set; }
    }
}

