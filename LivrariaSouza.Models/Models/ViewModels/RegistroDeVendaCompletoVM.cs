namespace LivrariaSouza.Models.Models.ViewModels
{
    public class RegistroDeVendaCompletoVM
    {
        public RegistroDeVendas RegistroDeVenda { get; set; }
        public IEnumerable<DetalhesVenda> ItensVenda { get; set; }
    }
}