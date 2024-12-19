using SerwisKupnoSprzedaz.Data;

namespace SerwisKupnoSprzedaz.Controllers
{
    public class OrderController
    {
        private readonly AppDBContext _dbContext;
        public OrderController(AppDBContext context) 
        { 
            _dbContext = context;
        }
    
        
    
    }
}
