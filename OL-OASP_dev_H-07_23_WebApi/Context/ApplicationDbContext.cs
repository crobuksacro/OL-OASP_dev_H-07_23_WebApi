using Microsoft.EntityFrameworkCore;
using OL_OASP_dev_H_07_23_WebApi.Models.Dbo;

namespace OL_OASP_dev_H_07_23_WebApi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
: base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
    }
}
