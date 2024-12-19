using SerwisKupnoSprzedaz.Data;
using SerwisKupnoSprzedaz.Models;

namespace SerwisKupnoSprzedaz.Services
{
    public class AnnouncmentService
    {
        private readonly AppDBContext appDBContext;

        public AnnouncmentService(AppDBContext context)
        {
            appDBContext = context;
        }
        public void CreateAnnouncment(int userID, Announcment ann)
        {

        }

        public void EditAnnouncment(int id, Announcment ann) { 

        }

        public IEnumerable<Announcment> GetAnnouncmentsByUser(int UserID) 
        {
            IEnumerable<Announcment> ann = new List<Announcment>();
            return ann;
        }

        public void DeleteAnnouncment(int id) { 
        
        }

        public Announcment GetAnnouncmentByID(int ID)
        {
            return new Announcment();   
        }
    }
}
