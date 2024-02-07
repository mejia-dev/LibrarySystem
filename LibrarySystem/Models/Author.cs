using System.Collections.Generic;

namespace LibrarySystem.Models
{
  public class Author
  {
    public int AuthorId { get; set; }
    public string AuthorFirstName { get; set; }
    public string AuthorLastName { get; set; }
    public List<Book> Books { get; set; }
  }
}