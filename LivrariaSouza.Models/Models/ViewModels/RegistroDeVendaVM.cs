namespace LivrariaSouza.Models.Models.ViewModels
{
    public class RegistroDeVendaVM
    {
        public int IdCompra { get; set; }
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime Data { get; set; }
        public decimal Total { get; set; }
    }
}
