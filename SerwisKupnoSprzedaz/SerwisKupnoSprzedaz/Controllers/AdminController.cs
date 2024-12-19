using Microsoft.AspNetCore.Mvc;
using SerwisKupnoSprzedaz.Data;

namespace SerwisKupnoSprzedaz.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDBContext _dbContext;

        public AdminController(AppDBContext context)
        {
            _dbContext = context;
        }

        public IActionResult Dashboard()
        {
            return View("AdminDashboard");
        }

        public IActionResult ManageAnnouncements()
        {
            var Announcements = _dbContext.Announcments.ToList();
            return View(Announcements);
        }

        
        public IActionResult ManageUsers()
        {
            var Users = _dbContext.Users.ToList();
            return View(Users);
        }

        public IActionResult ManageOrders()
        {
            var Orders = _dbContext.Orders.ToList();
            return View(Orders);
        }


    }


}
