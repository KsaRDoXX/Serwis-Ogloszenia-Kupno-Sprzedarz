using SerwisKupnoSprzedaz.Models;

public class Order
{
    public int Id { get; set; } // Klucz główny
    public string? OrderState { get; set; } 
    public string OrderAdress { get; set; } 
    public string DeliveryType { get; set; } 
    public DateTime? OrderDate { get; set; } 

    // Relacja z użytkownikiem
    public int UserId { get; set; } // Klucz obcy do tabeli User
    public virtual User? User { get; set; } 

    // Relacja z ogłoszeniem
    public int AnnouncementId { get; set; } // Klucz obcy do tabeli ogłoszenia
    public virtual Announcment? Announcement { get; set; } 
}
