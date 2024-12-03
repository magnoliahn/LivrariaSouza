//using LivrariaSouza.DataAccess;
//using LivrariaSouza.Models.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace LivrariaSouza.Controllers
//{
//    public class CarrinhoController : Controller
//    {
//        private readonly AppDbContext _db;
//        public CarrinhoController(AppDbContext db)
//        {
//            _db = db;
//        }

//        // Exibe o carrinho
//        public IActionResult ViewCarrinho()
//        {
//            var carrinho = HttpContext.Session.GetObjectFromJson<CarrinhoDeCompras>("Carrinho") ?? new CarrinhoDeCompras();

//            return View(carrinho);
//        }

//        public IActionResult AdicionarLivroAoCarrinho(int livroId, int qntdLivros, decimal valorLivro)
//        {
//            var livro = _db.Livros.FirstOrDefault(l => l.LivroId == livroId);
//            if (livro == null)
//            {
//                TempData["Mensagem"] = "Livro não encontrado.";
//                return RedirectToAction("ViewCarrinho");
//            }
//            if (qntdLivros <= 0)
//            {
//                TempData["Mensagem"] = "A quantidade deve ser um número inteiro e positivo.";
//                return RedirectToAction("ViewCarrinho");
//            }
//            var carrinho = HttpContext.Session.GetObjectFromJson<CarrinhoDeCompras>("Carrinho") ?? new CarrinhoDeCompras();

//            if (qntdLivros > livro.QntdEstoque)
//            {
//                TempData["Mensagem"] = $"Quantidade solicitada ({qntdLivros}) excede o estoque disponível ({livro.QntdEstoque}).";
//                return RedirectToAction("ViewCarrinho");
//            }
//            carrinho.AddLivroAoCarrinho(livroId, qntdLivros, valorLivro, livro.Titulo, livro.Imagem);
//            HttpContext.Session.SetObjectAsJson("Carrinho", carrinho);

//            TempData["Mensagem"] = "Livro adicionado ao carrinho com sucesso!";
//            return RedirectToAction("ViewCarrinho");
//        }



//        // Remove um item do carrinho
//        public IActionResult RemoveDoCarrinho(int livroId)
//        {
//            var carrinho = HttpContext.Session.GetObjectFromJson<CarrinhoDeCompras>("Carrinho") ?? new CarrinhoDeCompras();
//            carrinho.RemoverLivroDoCarrinho(livroId);
//            HttpContext.Session.SetObjectAsJson("Carrinho", carrinho);
//            return RedirectToAction("ViewCarrinho");
//        }

//        // Atualiza a quantidade de um item no carrinho
//        [HttpPost]
//        public IActionResult UpdateQuantidade(int livroId, int qntdLivros)
//        {
//            if (qntdLivros <= 0)
//            {
//                TempData["Mensagem"] = "A quantidade deve ser um número inteiro e positivo.";
//                return RedirectToAction("ViewCarrinho");
//            }
//            var livro = _db.Livros.FirstOrDefault(l => l.LivroId == livroId);
//            if (livro == null)
//            {
//                TempData["Mensagem"] = "Livro não encontrado.";
//                return RedirectToAction("ViewCarrinho");
//            }

//            if (qntdLivros > livro.QntdEstoque)
//            {
//                TempData["Mensagem"] = $"Quantidade solicitada ({qntdLivros}) excede o estoque disponível ({livro.QntdEstoque}).";
//                return RedirectToAction("ViewCarrinho");
//            }

//            var carrinho = HttpContext.Session.GetObjectFromJson<CarrinhoDeCompras>("Carrinho") ?? new CarrinhoDeCompras();
//            var item = carrinho.Itens.FirstOrDefault(i => i.IdItem == livroId);

//            if (item != null)
//            {
//                item.QntdItem = qntdLivros;
//            }
//            HttpContext.Session.SetObjectAsJson("Carrinho", carrinho);

//            TempData["Mensagem"] = "Quantidade atualizada com sucesso!";
//            return RedirectToAction("ViewCarrinho");
//        }
//        // Renderiza a página para finalizar compra
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
