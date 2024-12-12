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
        public Usuario Usuario { get; set; } // Prop de navegaçãos
        public string NomeUsuario { get; set; }
        public DateTime Data { get; set; }
        public decimal Total { get; set; }
    }

}