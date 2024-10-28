using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaSouza.Models.Models
{
    public class FinalizarCompra
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        [MaxLength(250, ErrorMessage = "O endereço não pode ter mais de 250 caracteres.")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "O CEP deve estar no formato 00000-000.")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "A forma de pagamento é obrigatória.")]
        public string FormaPagamento { get; set; }
    }
}
