using LivrariaSouza.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaSouza.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Livro> Livros { get; set; }
    }
}
