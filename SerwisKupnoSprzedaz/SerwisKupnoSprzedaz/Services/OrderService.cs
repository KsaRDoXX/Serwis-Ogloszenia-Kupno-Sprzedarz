using SerwisKupnoSprzedaz.Data;
using SerwisKupnoSprzedaz.Models;

namespace SerwisKupnoSprzedaz.Services
{
    public class OrderService
    {

        private readonly AppDBContext appDBContext;

        public OrderService(AppDBContext context)
        {
            appDBContext = context;
        }
        public void AddOrder(Order order)
        {

        }

        public void RemoveOrder(int ID) {
        
        }

        public void EditOrder(int ID, Order order)
        { 

        }

        public void DeleteOrder(int ID) {
        
        }

        public void VerifyOrder(int ID)
        {

        }


    }
}
