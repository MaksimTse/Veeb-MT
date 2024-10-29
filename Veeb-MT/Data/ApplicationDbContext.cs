using Microsoft.EntityFrameworkCore;
using Veeb_MT.Models;

namespace Veeb_MT.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Kasutaja> Kasutajad { get; set; }
        public DbSet<Toode> Tooted { get; set; }
    }
}