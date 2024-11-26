using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaSouza.Models.Models
{
    public class RegistroDeVendas
    {
        public int IdCompra { get; set; }  // chave primária
        public DateTime Data { get; set; }
        public decimal Total { get; set; }
        public string SessionId { get; set; }  // id único da sessão
        public ICollection<ItensDaCompra> Itens { get; set; }
    }

}
