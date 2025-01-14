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
            var announcments = _dbContext.Announcments.ToList();
            return View(announcments);
        }

        public IActionResult Edit(int id)
        {
            var announcement = _dbContext.Announcments.FirstOrDefault(a => a.Id == id);

            if (announcement == null)
            {
                return NotFound();
            }

            return View("EditAnnouncment", announcement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit(Announcment model)
        {
            if (ModelState.IsValid)
            {
                var existingAnnouncement = _dbContext.Announcments.FirstOrDefault(a => a.Id == model.Id);

                if (existingAnnouncement == null)
                {
                    return NotFound();
                }

                existingAnnouncement.Name = model.Name;
                existingAnnouncement.Description = model.Description;
                existingAnnouncement.Price = model.Price;
                existingAnnouncement.Category = model.Category;
                existingAnnouncement.Verified = model.Verified;

                _dbContext.SaveChanges();

                return RedirectToAction("AnnouncmentsList"); 
            }

            return View("EditAnnouncment", model);
        }


        //Przekierowanie do widoku dodawania
        [HttpGet]
        public IActionResult AddAnnouncment()
        {          
            return View();
        }

        //Funkcja dodająca ogłoszenie do bazy
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAnnouncment(Announcment model)
        {
            model.Verified=false;
            if (ModelState.IsValid)
            {
                //Id zalogowanego uzytkownika
                var userIdString = HttpContext.Session.GetString("UserId");

                int.TryParse(userIdString, out int userId);
               
                model.UserId = userId;

                _dbContext.Announcments.Add(model);
                _dbContext.SaveChanges();

                return RedirectToAction("AnnouncmentsList");

            }
            else
            {
                //Informacje o potencjalnych błędach
                Console.WriteLine(ModelState.ErrorCount);

                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"ModelState Error: {error.ErrorMessage}");
                }

                return View(model);
            }
                
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
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var announcement = _dbContext.Announcments.Find(id);
            if (announcement == null)
            {
                return NotFound();
            }

            _dbContext.Announcments.Remove(announcement);
            _dbContext.SaveChanges();

            // Pobranie adresu URL poprzedniej strony z nagłówka Referer
            var refererUrl = Request.Headers["Referer"].ToString();

            // Jeśli nagłówek Referer jest dostępny, przekieruj do poprzedniej strony, w przeciwnym razie do widoku domyślnego
            if (!string.IsNullOrEmpty(refererUrl))
            {
                return Redirect(refererUrl);
            }

            // Jeśli nie ma nagłówka Referer, przekieruj na stronę główną lub jakąkolwiek inną stronę
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Verify(int id)  
        {
            Console.WriteLine("ID = " + id);

            var announcement = _dbContext.Announcments.FirstOrDefault(a => a.Id == id);
            if (announcement != null)
            {
                announcement.Verified = true;
                _dbContext.SaveChanges();
            }

            return RedirectToAction("ManageAnnouncements", "Admin");
        }


    }
}
