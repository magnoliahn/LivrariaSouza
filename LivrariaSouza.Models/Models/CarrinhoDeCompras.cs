namespace LivrariaSouza.Models.Models
{
    public class CarrinhoDeCompras
    {
        public List<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();

        // Adiciona ou atualiza a quantidade de um item no carrinho
        public void AddLivroAoCarrinho(int livroId, int quantidade, decimal valor, string nomeLivro, string imagemLivro)
        {
            if (quantidade <= 0)
            {
                return;
            }
            var itemExistente = Itens.FirstOrDefault(i => i.IdItem == livroId);
            if (itemExistente != null)
            {
                itemExistente.QntdItem += quantidade;
            }
            else
            {
                Itens.Add(new ItemCarrinho
                {
                    IdItem = livroId,
                    QntdItem = quantidade,
                    ValorItem = valor,
                    NomeLivro = nomeLivro,
                    ImagemLivro = imagemLivro
                });
            }
        }

        // Remove um item do carrinho
        public void RemoverLivroDoCarrinho(int livroId)
        {
            Itens.RemoveAll(i => i.IdItem == livroId);
        }

        // Calcula o preço total dos itens
        public decimal ValorTotal => Itens.Sum(i => i.ValorItem * i.QntdItem);
    }
}

