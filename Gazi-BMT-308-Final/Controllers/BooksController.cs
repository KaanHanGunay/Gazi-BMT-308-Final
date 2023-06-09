﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Gazi_BMT_308_Final.Models;
using Gazi_BMT_308_Final.Services;
using Gazi_BMT_308_Final.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gazi_BMT_308_Final.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var books = !string.IsNullOrWhiteSpace(searchString)
                ? await _bookService.SearchBooks(searchString)
                : await _bookService.GetAllBooks();

            var viewModel = books.Select(b => new BookViewModel
            {
                Book = b,
                IsBorrowed = _bookService.IsBookBorrowedByUser(b.Id, userId).Result
            }).ToList();


            return View(viewModel);
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

        public async Task<IActionResult> BorrowBook(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _bookService.BorrowBook(id, int.Parse(userId));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ReturnBook(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _bookService.CreateReadingStatistic(userId, id);
            await _bookService.ReturnBook(id, userId);

            return RedirectToAction(nameof(Index));
        }

    }

}

