using System;
namespace Gazi_BMT_308_Final.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public string? Cover { get; set; }
        public DateTime DateAdded { get; set; }

        // Navigation property
        public ICollection<UserBook>? UserBooks { get; set; }
    }
}

