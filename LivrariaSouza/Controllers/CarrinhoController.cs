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
            var carrinho = HttpContext.Session.GetObjectFromJson<CarrinhoDeCompras>("Carrinho") ?? new CarrinhoDeCompras();

            return View(carrinho);
        }

        // Adiciona um item ao carrinho
        public IActionResult AdicionarLivroAoCarrinho(int livroId, int qntdLivros, decimal valorLivro)
        {
            var livro = _db.Livros.FirstOrDefault(l => l.Id == livroId);

            if (livro == null)
            {
                TempData["Mensagem"] = "Livro não encontrado.";
                return RedirectToAction("ViewCarrinho");
            }

            var carrinho = HttpContext.Session.GetObjectFromJson<CarrinhoDeCompras>("Carrinho") ?? new CarrinhoDeCompras();

            carrinho.AddLivroAoCarrinho(livroId, qntdLivros, valorLivro, livro.Titulo, livro.Imagem);

            HttpContext.Session.SetObjectAsJson("Carrinho", carrinho);

            return Ok("Livro adicionado ao carrinho com sucesso!");
        }

        // Remove um item do carrinho
        public IActionResult RemoveDoCarrinho(int livroId)
        {
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
                item.QntdItem = qntdLivros;
            }

            HttpContext.Session.SetObjectAsJson("Carrinho", carrinho);

            return RedirectToAction("ViewCarrinho");
        }

        // Renderiza a página para finalizar compra
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
