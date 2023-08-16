using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;

namespace MovieAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
