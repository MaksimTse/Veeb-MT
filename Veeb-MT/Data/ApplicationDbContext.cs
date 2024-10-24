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
        public DbSet<Ostukorv> Ostukorvid { get; set; }
        public DbSet<OstukorvToode> OstukorvTooted { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OstukorvToode>()
                .HasOne(ot => ot.Ostukorv)
                .WithMany(o => o.Items)
                .HasForeignKey(ot => ot.OstukorvId);

            modelBuilder.Entity<OstukorvToode>()
                .HasOne(ot => ot.Toode)
                .WithMany(t => t.OstukorvTooted)
                .HasForeignKey(ot => ot.ToodeId);

            modelBuilder.Entity<Ostukorv>()
                .HasOne(o => o.Kasutaja)
                .WithMany(k => k.Orders)
                .HasForeignKey(o => o.KasutajaId);
        }
    }
}
