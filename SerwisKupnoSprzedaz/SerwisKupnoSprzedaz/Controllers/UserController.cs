using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerwisKupnoSprzedaz.Data;
using SerwisKupnoSprzedaz.Models;
using SerwisKupnoSprzedaz.Services;
using System.Collections.Specialized;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        //Przekierowanie do widoku Login
        [AllowAnonymous]

        public IActionResult Login()
        {                        
            return View();
        }

       
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _dbContext.Users
                    .FirstOrDefault(u => u.UserName == model.UserName && u.PasswordHash == model.Password);

                if (user != null)
                {                  
                    //Zalogowano
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    HttpContext.Session.SetString("UserName", user.UserName);
                    
                    ViewBag.UserName = user.UserName;

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Nieprawidłowe dane logowania.");
            }

            return View(model);
        }

        //Przekierowanie do widoku rejestracji
        [AllowAnonymous]
        public IActionResult Register()
        {
                return View();
        }

        //Funkcja rejestrująca podane dane użytkownika
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User model)
        {
            model.IsAdmin = false;

            if (ModelState.IsValid)
            {
                // Dodaj ogłoszenie do bazy danych
                _dbContext.Users.Add(model);
                _dbContext.SaveChanges();

                // Przekierowanie po zapisaniu
                return RedirectToAction("Login");
            } 
                        
            return View(model);
        }


        // Usuwa wszystkie dane z sesji
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(a => a.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();

            return RedirectToAction("ManageUsers","Admin");
        }

        public IActionResult Edit(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(a => a.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View("EditUser", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit(User model)
        {
            //test
            Console.WriteLine(model.Id);

            if (ModelState.IsValid)
            {
                var existingUser = _dbContext.Users.FirstOrDefault(a => a.Id == model.Id);

                if (existingUser == null)
                {
                    return NotFound();
                }

                existingUser.UserName = model.UserName;
                existingUser.PasswordHash = model.PasswordHash;
                existingUser.Email = model.Email;
                existingUser.IsAdmin = model.IsAdmin;


                _dbContext.SaveChanges();


                return RedirectToAction("Edit", model);
            }

            return RedirectToAction("Edit", model);
        }
        

        public IActionResult Details(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(a => a.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View("UserDetails", user);
        }


        // Akcja, która zwraca zamówienia użytkownika
        public IActionResult Orders(int id)
        {
            var user = _dbContext.Users
                .Include(u => u.Orders)
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var orders = user.Orders; // Pobierz zamówienia powiązane z użytkownikiem
            return View("OrderList", orders); // Przekieruj do widoku OrderList.cshtml
        }

        public IActionResult Announcements(int id)
        {
            var user = _dbContext.Users
                .Include(u => u.Orders)
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var announcements = user.Announcments; // Pobierz zamówienia powiązane z użytkownikiem
            return View("UserAnnouncements", announcements); // Przekieruj do widoku AnnouncementsList
        }
    }
}
