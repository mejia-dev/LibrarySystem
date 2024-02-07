using Microsoft.EntityFrameworkCore;
using LibrarySystem.Models;

namespace LibrarySystem.Models
{
  public class LibrarySystemContext : DbContext
  {
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Patron> Patrons { get; set; }
    public DbSet<BookPatron> BookPatrons { get; set; }
    public LibrarySystemContext(DbContextOptions options) : base(options) { }
  }
}