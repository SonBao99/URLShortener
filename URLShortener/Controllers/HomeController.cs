using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using URLShortener.Models;

namespace URLShortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UrlContext _urlContext;

        public HomeController(ILogger<HomeController> logger, UrlContext urlContext)
        {
            _logger = logger;
            _urlContext = urlContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        //public IActionResult MyUrls()
        //{
        //    var userId = User.Identity?.Name;
        //    var urls = _urlContext.Urls
        //        .Where(u => u.UserId == userId)
        //        .OrderByDescending(u => u.CreatedAt)
        //        .ToList();
        //    return View(urls);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}