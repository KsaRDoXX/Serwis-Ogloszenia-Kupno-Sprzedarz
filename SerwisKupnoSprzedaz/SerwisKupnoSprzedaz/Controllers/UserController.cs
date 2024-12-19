using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerwisKupnoSprzedaz.Data;
using SerwisKupnoSprzedaz.Services;
using System.Collections.Specialized;

namespace SerwisKupnoSprzedaz.Controllers
{
   
    public class UserController : Controller
    {
        private readonly AppDBContext _dbContext;

        public UserController(AppDBContext _context)
        {
            _dbContext = _context;
        }

        UserService userService;

        public IActionResult Login(string login, string password, string password2 = "", string adress = "")
        {
            ViewBag.Username = string.Format("{0} - {1} - {2} - {3}", login, password, password2, adress);
                        
            return View();

        }

        public IActionResult Register(string userName, string password, string password2, string adress)
        {
                return View("Register");
        }
    }
}
