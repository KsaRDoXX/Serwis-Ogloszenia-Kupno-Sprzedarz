using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SerwisKupnoSprzedaz.Models
{
    public class Announcment
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Wymagana nazwa")]
        [MaxLength(100)]

        public string Name { get; set; }
        [Required(ErrorMessage ="Opis Wymagany")]
        public string Description { get; set; }
        [Required(ErrorMessage ="Cena wymagana")]
        public float Price { get; set; }
        [Required(ErrorMessage ="Kategoria wymagana")]
        public string Category { get; set; }
        public bool Verified { get; set; }
    
        public int UserId { get; set; }
        
        public virtual User? User { get; set; }

        public virtual Order? Order { get; set; }

    }
}
