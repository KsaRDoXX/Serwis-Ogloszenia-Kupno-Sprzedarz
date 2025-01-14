using Microsoft.AspNetCore.Mvc;
using SerwisKupnoSprzedaz.Data;
using SerwisKupnoSprzedaz.Models;
using System.Diagnostics;

namespace SerwisKupnoSprzedaz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

          private readonly AppDBContext _dbContext;

   
        public HomeController(ILogger<HomeController> logger, AppDBContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        //Inicjalizacja widoku g³ównego
        public IActionResult Index()
        {
            var userName = HttpContext.Session.GetString("UserName");

            // Przekazanie nazwy u¿ytkownika do layoutu
            ViewBag.UserName = userName;
            var announcements = _dbContext.Announcments.ToList();
            return View(announcements);
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
    }
}
