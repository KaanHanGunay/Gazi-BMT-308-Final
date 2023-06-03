using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Gazi_BMT_308_Final.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}

