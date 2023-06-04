using System;
namespace Gazi_BMT_308_Final.Models
{
    public class ReadingStatistics
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
    }
}
