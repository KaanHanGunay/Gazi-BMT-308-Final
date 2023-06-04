using System;
using Gazi_BMT_308_Final.Models;

namespace Gazi_BMT_308_Final.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<Book> LatestBooks { get; set; }
        public List<Book> MostReadBooks { get; set; }
        public List<User> MostReadingUsers { get; set; }
    }

}

