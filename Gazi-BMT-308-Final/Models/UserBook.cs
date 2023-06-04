using System;
namespace Gazi_BMT_308_Final.Models
{
    public class UserBook
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public DateTime BorrowDate { get; set; }
    }
}

