using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivrariaSouza.Models.Models
{
    public class DetalhesVenda
    {
        [Key]
        public int IdDetalhe { get; set; }

        [ForeignKey("RegistroDeVendas")]
        public int IdRegistroVenda { get; set; }
        public RegistroDeVendas RegistroDeVendas { get; set; } // Prop de navegação

        [ForeignKey("Livro")]
        public int LivroId { get; set; }
        public Livro Livro { get; set; } // Prop de navegação
        public string Titulo { get; set; }
        public string CapaLivro { get; set; }
        public decimal ValorUnit { get; set; }
        public int Quantidade { get; set; }
    }
}
