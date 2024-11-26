using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaSouza.Models.Models
{
    public class ItensDaCompra
    {
        public int IdItem { get; set; }  // chave primária
        public int IdCompra { get; set; }  // chave estrangeira
        public int LivroId { get; set; }  // chave estrangeira para livro
        public string Titulo { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }
        public decimal Subtotal { get; set; }
        public Livro Livro { get; set; }
    }

}
