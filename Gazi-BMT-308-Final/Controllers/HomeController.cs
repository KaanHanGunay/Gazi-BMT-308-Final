using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gazi_BMT_308_Final.Models;
using Microsoft.AspNetCore.Authorization;

namespace Gazi_BMT_308_Final.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
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

