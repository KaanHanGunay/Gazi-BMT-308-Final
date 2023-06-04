using System;
using Gazi_BMT_308_Final.Models;
using Microsoft.EntityFrameworkCore;

namespace Gazi_BMT_308_Final.Services
{
    public class BookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Book> CreateBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<Book> GetBook(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task UpdateBook(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Book>> SearchBooks(string searchString)
        {
            var lowerCaseSearchString = searchString.ToLower();

            return await _context.Books
                .Where(b => b.Title.ToLower().Contains(lowerCaseSearchString)
                            || b.Author.ToLower().Contains(lowerCaseSearchString))
                .ToListAsync();
        }

        public async Task<UserBook> BorrowBook(int bookId, int userId)
        {
            var userBook = new UserBook { BookId = bookId, UserId = userId };
            _context.UserBooks.Add(userBook);
            await _context.SaveChangesAsync();

            return userBook;
        }

        public async Task<bool> IsBookBorrowedByUser(int bookId, int userId)
        {
            return await _context.UserBooks
                .AnyAsync(ub => ub.BookId == bookId && ub.UserId == userId);
        }

        public async Task ReturnBook(int bookId, int userId)
        {
            var userBook = await _context.UserBooks
                .SingleOrDefaultAsync(ub => ub.BookId == bookId && ub.UserId == userId);

            if (userBook != null)
            {
                _context.UserBooks.Remove(userBook);
                await _context.SaveChangesAsync();
            }
        }

    }

}

