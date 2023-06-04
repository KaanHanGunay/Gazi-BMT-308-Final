using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gazi_BMT_308_Final.Models;
using Gazi_BMT_308_Final.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gazi_BMT_308_Final.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var books = !string.IsNullOrWhiteSpace(searchString)
                ? await _bookService.SearchBooks(searchString)
                : await _bookService.GetAllBooks();

            return View(books);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Author,PublicationYear,Cover")] Book book)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors)
                                       .Select(x => x.ErrorMessage)
                                       .ToList();

                // errors listesi şimdi ModelState hatalarını içeriyor.
                // Her bir hatayı aşağıdaki gibi konsola yazdırabilirsiniz:
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }
            }


            if (ModelState.IsValid)
            {
                await _bookService.CreateBook(book);
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBook(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,PublicationYear,Cover")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bookService.UpdateBook(book);
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetBook(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }

    }

}

