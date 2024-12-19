using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerwisKupnoSprzedaz.Data;
using SerwisKupnoSprzedaz.Models;

namespace SerwisKupnoSprzedaz.Controllers
{
    public class AnnouncmentsController : Controller
    {
        private readonly AppDBContext _dbContext;

        public AnnouncmentsController(AppDBContext context)
        {
            _dbContext = context;
        }
        public IActionResult AnnouncmentsList()
        {
            var announcments = _dbContext.Announcments.ToList();
            return View(announcments);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View("AddAnnouncment");            
        }

        public IActionResult Edit(int ID)
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddAnnouncment()
        {          
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAnnouncment(Announcment model)
        {
            if (ModelState.IsValid)
            {
                // Dodaj ogłoszenie do bazy danych
                _dbContext.Announcments.Add(model);
                _dbContext.SaveChanges();

                // Przekierowanie po zapisaniu
                return RedirectToAction("AnnouncmentsList");
            }
                
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var announcement = _dbContext.Announcments.FirstOrDefault(a => a.Id == id);

            if (announcement == null)
            {
                return NotFound();
            }

            return View("AnnouncementsDetailsUser", announcement);
        }

        public IActionResult AdminDetails(int id)
        {
            var announcement = _dbContext.Announcments.FirstOrDefault(a => a.Id == id);

            if (announcement == null)
            {
                return NotFound();
            }

            return View("AnnouncementsDetailsAdmin", announcement);
        }

        [HttpPost]
        public IActionResult Delete(int announcementId)
        {
            var announcement = _dbContext.Announcments.FirstOrDefault(a => a.Id == announcementId);
            if (announcement != null)
            {
                _dbContext.Announcments.Remove(announcement);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Verify(int announcementId)
        {
            var announcement = _dbContext.Announcments.FirstOrDefault(a => a.Id == announcementId);
            if (announcement != null)
            {
                announcement.Verified = true;
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }



    }
}
