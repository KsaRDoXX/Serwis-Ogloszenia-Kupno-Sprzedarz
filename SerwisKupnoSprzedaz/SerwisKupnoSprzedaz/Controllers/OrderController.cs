using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerwisKupnoSprzedaz.Data;
using SerwisKupnoSprzedaz.Models;
using System.Collections.Specialized;

namespace SerwisKupnoSprzedaz.Controllers
{
    public class OrderController : Controller
    {
        int tempAnnouncementId;

        private readonly AppDBContext _dbContext;
        public OrderController(AppDBContext context) 
        { 
            _dbContext = context;
        }
        
        //Przekierowanie do formularza tworzenia
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrder(int id) // Oczekujemy "id" zgodnie z asp-route-id w formularzu
        {
            var order = new Order
            {
                AnnouncementId = id // Przypisujemy przekazane ID ogłoszenia
            };

            return View(order); // Przekazujemy model do widoku
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order model)
        {
            // Pobierz UserId z sesji
            var userIdString = HttpContext.Session.GetString("UserId");
            if (!int.TryParse(userIdString, out int userId))
            {
                // Jeśli nie ma UserId w sesji, przekieruj do logowania
                return RedirectToAction("Login", "User");
            }

            // Uzupełnij model o brakujące dane
            model.UserId = userId;
            model.OrderState = "Przyjęty"; // Przykładowy stan zamówienia
            model.OrderDate = model.OrderDate == DateTime.MinValue ? DateTime.Now : model.OrderDate;

            model.OrderDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                // Dodaj zamówienie do bazy danych
                _dbContext.Orders.Add(model);
                _dbContext.SaveChanges();

                // Przekieruj do widoku potwierdzenia lub szczegółów zamówienia
                return RedirectToAction("OrderDetails", "Order", new { id = model.Id });
            }
            else
            {
                // Wyświetl błędy walidacji i wróć do widoku AddOrder
                return View("AddOrder", model);
            }
        }
        public IActionResult OrderDetails(int id)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View("OrderDetails",order);
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View("EditOrder", order); // Jawne wskazanie widoku EditOrder.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Order model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditOrder", model); // Wraca do EditOrder.cshtml w przypadku błędów
            }

            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == model.Id);
            if (order == null)
            {
                return NotFound();
            }

            // Aktualizuj właściwości zamówienia
            order.OrderDate = model.OrderDate;
            order.DeliveryType = model.DeliveryType;
            order.OrderAdress = model.OrderAdress;
            order.OrderState = model.OrderState;

            _dbContext.SaveChanges(); // Zapisz zmiany w bazie

            return RedirectToAction("OrderDetails", new { id = model.Id }); // Przekieruj do szczegółów zamówienia
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();

            TempData["Message"] = "Zamówienie zostało usunięte.";
            return RedirectToAction("Orders", "User", new { id = order.UserId });
        }

    }
}
