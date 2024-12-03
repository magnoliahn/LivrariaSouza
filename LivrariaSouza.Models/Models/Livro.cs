using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivrariaSouza.Models.Models
{
    public class Livro
    {
        [Key]
        public int LivroId { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string CapaLivro { get; set; }
        [Required]
        public string Autor { get; set; }
        [Required]
        public int NumeroPag { get; set; }

        [NotMapped] // Isso impede que a propriedade seja mapeada no banco de dados
        public string? ValorVendaString { get; set; } // Propriedade temporária para entrada
        [NotMapped] // Isso impede que a propriedade seja mapeada no banco de dados
        public string? ValorCompraString { get; set; } // Propriedade temporária para entrada

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor de venda deve ser positivo.")]
        public decimal ValorVenda { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor de compra deve ser positivo.")]
        public decimal ValorCompra { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Livro), nameof(ValidarAnoLancamento))]
        public DateTime DataLancamento { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque não pode ser negativa.")]
        [Required]
        public int QntdEstoque { get; set; }
        [Required]
        [MaxLength(500)]
        public string Descricao { get; set; }


        public static ValidationResult ValidarAnoLancamento(DateTime anoLancamento, ValidationContext context)
        {
            if (anoLancamento > DateTime.Now)
            {
                return new ValidationResult("Data de lançamento inválida.");
            }
            return ValidationResult.Success;
        }

    }
}