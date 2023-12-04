using Microsoft.EntityFrameworkCore;
using PustokBookStore.Models;

namespace PustokBookStore.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<BookSliderModel> BookSliders { get; set; }
        public AppDbContext(DbContextOptions options) : base(options) { }
      
    }
}
