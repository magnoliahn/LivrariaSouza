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

        //Exibe o carrinho
        public IActionResult ViewCarrinho(int userId)
        {
            if (userId == 0)
            {
                userId = 1;
            }
            var carrinho = _db.Carrinhos.Include(c => c.Livro).Where(c => c.IdUsuario == userId).ToList();

            if (carrinho == null || !carrinho.Any())
            {
                carrinho = new List<Carrinho>();
            }

            return View(carrinho);
        }
        public IActionResult AdicionarLivroAoCarrinho(int userId, int livroId, int quantidade)
        {
            userId = 1;
            var livro = _db.Livros.FirstOrDefault(l => l.LivroId == livroId);

            if (livro == null || quantidade <= 0)
            {
                TempData["Mensagem"] = livro == null
                    ? $"Erro: O livro {livro.Titulo} não foi encontrado."
                    : "A quantidade deve ser maior que zero. Por favor, insira um valor válido.";
                return RedirectToAction("ViewCarrinho", new { userId });
            }

            var carrinho = _db.Carrinhos.FirstOrDefault(c => c.LivroId == livroId && c.IdUsuario == userId);


            if (carrinho != null)
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

            else
            {
                carrinho = new Carrinho
                {
                    LivroId = livroId,
                    IdUsuario = userId,
                    Quantidade = quantidade,
                    ValorUnit = livro.ValorVenda,
                    Subtotal = livro.ValorVenda * quantidade,

                };

                _db.Carrinhos.Add(carrinho);
            }


            try
            {
                _db.SaveChanges();
                TempData["Mensagem"] = "Livro adicionado ao carrinho com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = $"Erro ao salvar o carrinho: {ex.Message}";
            }

            return RedirectToAction("ViewCarrinho", new { userId });
        }



        public IActionResult RemoveDoCarrinho(int userId, int livroId)
        {
            if (userId == 0)
            {
                userId = 1;
            }
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
                carrinho.Subtotal = carrinho.Quantidade * carrinho.ValorUnit;
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
