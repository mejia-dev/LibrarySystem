using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Models
{
  public class LibrarySystemContext : DbContext
  {
    public DbSet<Author> Authors { get; set; }
    public LibrarySystemContext(DbContextOptions options) : base(options) { }
  }
}