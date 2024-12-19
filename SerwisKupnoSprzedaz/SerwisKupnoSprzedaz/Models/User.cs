namespace SerwisKupnoSprzedaz.Models
{
    public class User
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public bool IsAdmin { get; set; }   

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Announcment> Announcments { get; set; }


    }
}
