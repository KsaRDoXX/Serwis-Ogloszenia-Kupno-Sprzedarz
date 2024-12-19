using System.ComponentModel.DataAnnotations;

namespace SerwisKupnoSprzedaz.Models
{
    public class Announcment
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Category { get; set; }
        public bool Verified { get; set; }
    
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual Order Order { get; set; }

    }
}
