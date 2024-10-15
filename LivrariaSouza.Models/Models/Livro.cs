using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivrariaSouza.Models.Models
{
    public class Livro
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Imagem { get; set; }
        [Required]
        public string Autor {  get; set; }
        [Required]
        public int NumeroPag {  get; set; }

        [NotMapped] // Isso impede que a propriedade seja mapeada no banco de dados
        public string ValorString { get; set; } // Propriedade temporária para entrada

        [Required]
        public decimal Valor { get; set; }
        [Required]
        public DateTime AnoLancamento { get; set; }
        [Required]
        public int QntdEstoque { get; set; }
        [Required]
        [MaxLength(500)]
        public string Descricao { get; set; }

    }
}
