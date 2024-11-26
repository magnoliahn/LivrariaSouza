using LivrariaSouza.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaSouza.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<RegistroDeVendas> RegistroDeVendas { get; set; }
        public DbSet<ItensDaCompra> ItensDaCompra { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItensDaCompra>().HasKey(i => i.IdItem);

            modelBuilder.Entity<RegistroDeVendas>().HasKey(r => r.IdCompra);

        }

    }
}
