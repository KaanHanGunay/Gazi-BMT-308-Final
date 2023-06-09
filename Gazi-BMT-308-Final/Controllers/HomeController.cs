﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gazi_BMT_308_Final.Models;
using Microsoft.AspNetCore.Authorization;
using Gazi_BMT_308_Final.Services;
using Gazi_BMT_308_Final.ViewModels;

namespace Gazi_BMT_308_Final.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BookService _bookService;

    public HomeController(ILogger<HomeController> logger, BookService bookService)
    {
        _logger = logger;
        _bookService = bookService;
    }

    public async Task<IActionResult> Index()
    {
        var latestBooks = await _bookService.GetLatestBooks();
        var mostReadBooks = await _bookService.GetMostReadBooks();
        var mostReadingUsers = await _bookService.GetMostReadingUsers();
        var model = new HomeIndexViewModel
        {
            LatestBooks = latestBooks,
            MostReadBooks = mostReadBooks,
            MostReadingUsers = mostReadingUsers
        };
        return View(model);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult About()
    {
        ViewBag.Name = "Kaan Han Günay";
        ViewBag.StudentNumber = "201816074";
        ViewBag.Email = "kaanh.gunay@gmail.com";
        return View();
    }

}

