using LivrariaSouza.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaSouza.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<RegistroDeVendas> RegistroDeVendas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<RegistroDeVendas>().HasKey(r => r.IdCompra);
            modelBuilder.Entity<Carrinho>(entity =>
            {
                // Chave composta
                entity.HasKey(c => new { c.IdCarrinho, c.LivroId, c.IdUsuario });

                // Configurar FK para Livro
                entity.HasOne(c => c.Livro)
                      .WithMany() // Um livro pode estar em vários carrinhos
                      .HasForeignKey(c => c.LivroId);

                // Configurar FK para Comprador
                entity.HasOne(c => c.Usuario)
                      .WithMany() // Um comprador pode ter vários itens no carrinho
                      .HasForeignKey(c => c.IdUsuario);



            });
        }
    }
}