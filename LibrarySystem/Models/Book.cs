using System.Collections.Generic;

namespace LibrarySystem.Models
{
  public class Book
  {
    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public string BookPublisher { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
    public List<BookPatron> JoinEntities { get; set; }
  }
}