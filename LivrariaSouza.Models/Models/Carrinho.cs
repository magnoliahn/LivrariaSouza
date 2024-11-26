using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaSouza.Models.Models
{
    public class Carrinho
    {
        public int CarrinhoId { get; set; } 
        public int LivroId { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Titulo { get; set; }
        public string Imagem { get; set; }
        public string SessionId { get; set; }  // id único da sessão
    }

}
