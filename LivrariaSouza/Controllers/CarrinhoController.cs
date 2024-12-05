using LivrariaSouza.DataAccess;
using LivrariaSouza.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult ViewCarrinho(int userId)
        {
            var carrinho = _db.Carrinhos.Include(c => c.Livro).Where(c => c.IdUsuario == userId).ToList();

            return View(carrinho);
        }

        public IActionResult AdicionarLivroAoCarrinho(int userId, int livroId, int quantidade)
        {
            var livro = _db.Livros.FirstOrDefault(l => l.LivroId == livroId);

            if (livro == null)
            {
                TempData["Mensagem"] = "Livro não encontrado.";
                return RedirectToAction("ViewCarrinho", new { userId });
            }
            if (quantidade <= 0)
            {
                TempData["Mensagem"] = "A quantidade deve ser um número inteiro e positivo.";
                return RedirectToAction("ViewCarrinho", new { userId });
            }
            var carrinho = _db.Carrinhos.FirstOrDefault(c => c.LivroId == livroId && c.IdUsuario == userId);

            if (carrinho == null)
            {
                carrinho = new Carrinho
                {
                    LivroId = livroId,
                    IdUsuario = userId,
                    Quantidade = quantidade,
                    ValorUnit = livro.ValorVenda,
                    Subtotal = (int)(livro.ValorVenda * quantidade)
                };

                _db.Carrinhos.Add(carrinho);
            }
            else
            {
                int livrosNoCarrinho = carrinho.Quantidade + quantidade;
                if (livrosNoCarrinho > livro.QntdEstoque)
                {
                    TempData["Mensagem"] = $"A quantidade adicionada ao carrinho de ({livrosNoCarrinho}) excede o estoque disponível ({livro.QntdEstoque}).";
                    return RedirectToAction("ViewCarrinho", new { userId });
                }
                carrinho.Quantidade += quantidade;
                carrinho.Subtotal = (int)(carrinho.Quantidade * carrinho.ValorUnit);
            }
            _db.SaveChanges();
            TempData["Mensagem"] = "Livro adicionado ao carrinho com sucesso!";
            return RedirectToAction("ViewCarrinho", new { userId });
        }


        public IActionResult RemoveDoCarrinho(int userId, int livroId)
        {
            var carrinho = _db.Carrinhos.FirstOrDefault(c => c.LivroId == livroId && c.IdUsuario == userId);

            if (carrinho != null)
            {
                _db.Carrinhos.Remove(carrinho);
                _db.SaveChanges();
            }

            return RedirectToAction("ViewCarrinho", new { userId });
        }

        // Atualiza a quantidade de um item no carrinho
        [HttpPost]
        public IActionResult AtualizaQuantidade(int userId, int livroId, int quantidade)
        {
            if (quantidade <= 0)
            {
                TempData["Mensagem"] = "A quantidade deve ser um número inteiro e positivo.";
                return RedirectToAction("ViewCarrinho", new { userId });
            }

            var carrinho = _db.Carrinhos.FirstOrDefault(c => c.LivroId == livroId && c.IdUsuario == userId);

            if (carrinho != null)
            {
                carrinho.Quantidade = quantidade;
                carrinho.Subtotal = (int)(carrinho.ValorUnit * quantidade);

                _db.SaveChanges();
            }

            TempData["Mensagem"] = "Quantidade atualizada com sucesso!";
            return RedirectToAction("ViewCarrinho", new { userId });
        }
    }
}

        // Renderiza a página para finalizar compra
//        [HttpGet]
//        public IActionResult FinalizarCompra()
//        {
//            return View();
//        }

//        // Processa a finalização da compra
//        [HttpPost]
//        public IActionResult FinalizarCompra(FinalizarCompra model, CarrinhoDeCompras carrinho)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }
//            UpdateStock(carrinho);
//            HttpContext.Session.Remove("Carrinho");

//            TempData["Mensagem"] = "Compra finalizada com sucesso!";
//            return RedirectToAction("ViewCarrinho");
//        }

//        public void UpdateStock(CarrinhoDeCompras carrinho)
//        {
//            carrinho = HttpContext.Session.GetObjectFromJson<CarrinhoDeCompras>("Carrinho") ?? new CarrinhoDeCompras();

//            foreach (var item in carrinho.Itens)
//            {
//                var book = _db.Livros.FirstOrDefault(b => b.LivroId == item.IdItem);
//                if (book != null)
//                {
//                    book.QntdEstoque = book.QntdEstoque - item.QntdItem;
//                    _db.SaveChanges();
//                }
//            }
//        }
//    }
//}
