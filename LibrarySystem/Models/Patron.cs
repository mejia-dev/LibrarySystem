using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models
{
    public class Patron
  {
    public int PatronId { get; set; }
    public string PatronName { get; set; }
    public string LibraryCard { get; set; }
    public List<BookPatron> JoinEntites { get; set; }
  }
}

