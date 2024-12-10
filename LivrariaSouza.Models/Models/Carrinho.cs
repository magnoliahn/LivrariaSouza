using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivrariaSouza.Models.Models
{
    public class Carrinho
    {
        [Key]
        public int IdCarrinho { get; set; }

        [ForeignKey("Livro")]
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
        
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnit { get; set; }
        public decimal Subtotal { get; set; }

    }
}