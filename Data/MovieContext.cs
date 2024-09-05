using Microsoft.EntityFrameworkCore;
using MovieTestCRUDMethodsForDB.Models;

namespace MovieTestCRUDMethodsForDB.Data
{
    public class MovieContext(DbContextOptions<MovieContext> options) : DbContext(options)
    {
        public DbSet<Movie> Movies { get; set; }
    }
}
