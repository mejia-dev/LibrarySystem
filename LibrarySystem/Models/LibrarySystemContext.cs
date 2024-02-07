using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Models
{
  public class LibrarySystemContext : DbContext
  {
    // public DbSet<Student> Students { get; set; }
    public LibrarySystemContext(DbContextOptions options) : base(options) { }
  }
}