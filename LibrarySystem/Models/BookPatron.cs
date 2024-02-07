using System;

namespace LibrarySystem.Models
{
  public class BookPatron
  {
    public int BookPatronId { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int PatronId { get; set; }
    public Patron Patron { get; set; }
    public DateTime Checkout { get; set; }
    public DateTime ReturnDate { get; set; }
    public void GetReturnDate()
    {
      ReturnDate = Checkout.AddDays(7);
    }
  }

  
}