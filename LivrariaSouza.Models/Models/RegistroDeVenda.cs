using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivrariaSouza.Models.Models
{
    public class RegistroDeVendas
    {
        [Key]
        public int IdCompra { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        public DateTime Data { get; set; }
        public decimal Total { get; set; }
        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public string CapaLivro { get; set; }
        public int ValorUnit { get; set; }
        public int Quantidade { get; set; }
    }

}