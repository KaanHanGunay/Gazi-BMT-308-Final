using System;
namespace Gazi_BMT_308_Final.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        // Navigation property
        public ICollection<UserBook> UserBooks { get; set; }
    }
}

