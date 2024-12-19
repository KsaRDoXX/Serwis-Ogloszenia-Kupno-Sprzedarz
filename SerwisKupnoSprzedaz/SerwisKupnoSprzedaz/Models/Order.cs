using SerwisKupnoSprzedaz.Models;

public class Order
{
    public int Id { get; set; } // Klucz główny
    public string OrderState { get; set; } // Stan zamówienia
    public string OrderAdress { get; set; } // Adres zamówienia
    public string DeliveryType { get; set; } // Typ dostawy
    public DateTime? OrderDate { get; set; } // Data zamówienia (nullable)

    // Relacja z użytkownikiem
    public int UserId { get; set; } // Klucz obcy do tabeli User
    public virtual User User { get; set; } // Nawigacyjna właściwość do użytkownika

    // Relacja z ogłoszeniem
    public int AnnouncementId { get; set; } // Klucz obcy do tabeli Announcement
    public virtual Announcment Announcement { get; set; } // Nawigacyjna właściwość do ogłoszenia
}
