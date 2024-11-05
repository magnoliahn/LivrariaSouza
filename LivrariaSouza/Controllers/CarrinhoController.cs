using LivrariaSouza.DataAccess;
using LivrariaSouza.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaSouza.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly AppDbContext _db;
        public CarrinhoController(AppDbContext db)
        {
            _db = db;
        }

        // Exibe o carrinho
        public IActionResult ViewCarrinho()
        {
            // Inicializa o carrinho sempre que a view é chamada
            var carrinho = HttpContext.Session.GetObjectFromJson<CarrinhoDeCompras>("Carrinho") ?? new CarrinhoDeCompras();

            // Retorna a view com o carrinho
            return View(carrinho); // Aqui, estamos passando o carrinho como Model para a view
        }

        // Adiciona um item ao carrinho
        public IActionResult AdicionarLivroAoCarrinho(int livroId, int qntdLivros, decimal valorLivro)
        {
            // Recupera o livro do banco de dados
            var livro = _db.Livros.FirstOrDefault(l => l.Id == livroId);

            // Verifica se o livro foi encontrado
            if (livro == null)
            {
                TempData["Mensagem"] = "Livro não encontrado.";
                return RedirectToAction("ViewCarrinho"); // Ou redirecione para onde você desejar
            }

            // Recupera o carrinho da sessão ou cria um novo se não existir
            var carrinho = HttpContext.Session.GetObjectFromJson<CarrinhoDeCompras>("Carrinho") ?? new CarrinhoDeCompras();

            // Adiciona o item ao carrinho, incluindo informações do livro
            carrinho.AddLivroAoCarrinho(livroId, qntdLivros, valorLivro, livro.Titulo, livro.Imagem);

            // Salva o carrinho atualizado na sessão
            HttpContext.Session.SetObjectAsJson("Carrinho", carrinho);

            return Ok("Livro adicionado ao carrinho com sucesso!");
        }

        // Remove um item do carrinho
        public IActionResult RemoveDoCarrinho(int livroId)
        {
            // Recupera o carrinho da sessão ou cria um novo se não existir
            var carrinho = HttpContext.Session.GetObjectFromJson<CarrinhoDeCompras>("Carrinho") ?? new CarrinhoDeCompras();
            carrinho.RemoverLivroDoCarrinho(livroId);
            HttpContext.Session.SetObjectAsJson("Carrinho", carrinho);
            return RedirectToAction("ViewCarrinho");
        }

        // Atualiza a quantidade de um item no carrinho
        [HttpPost]
        public IActionResult UpdateQuantidade(int livroId, int qntdLivros)
        {
            var carrinho = HttpContext.Session.GetObjectFromJson<CarrinhoDeCompras>("Carrinho") ?? new CarrinhoDeCompras();
            var item = carrinho.Itens.FirstOrDefault(i => i.IdItem == livroId);

            if (item != null)
            {
                item.QntdItem = qntdLivros; // Atualiza a quantidade
            }

            HttpContext.Session.SetObjectAsJson("Carrinho", carrinho);

            return RedirectToAction("ViewCarrinho");
        }

        [HttpGet]
        public IActionResult FinalizarCompra()
        {
            return View();
        }

        // Processa a finalização da compra
        [HttpPost]
        public IActionResult FinalizarCompra(FinalizarCompra model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            HttpContext.Session.Remove("Carrinho");

            TempData["Mensagem"] = "Compra finalizada com sucesso!";
            return RedirectToAction("ViewCarrinho");
        }

    }
}
