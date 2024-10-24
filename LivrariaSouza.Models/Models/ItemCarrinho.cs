namespace LivrariaSouza.Models.Models
{
    public class ItemCarrinho
    {
        public int IdItem { get; set; }
        public int QntdItem { get; set; }
        public decimal ValorItem { get; set; }
        public string NomeLivro { get; set; }
        public string ImagemLivro { get; set; }
    }
}
