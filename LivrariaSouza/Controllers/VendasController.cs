using LivrariaSouza.DataAccess;
using LivrariaSouza.Models.Models;
using LivrariaSouza.Models.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaSouza.Controllers
{
    public class VendasController : Controller
    {
        private readonly AppDbContext _db;

        public VendasController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult FinalizarCompra()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Finalizar(FinalizarCompra model)
        {
            int usuarioId = 1;

            if (ModelState.IsValid)
            {
                // Lógica para processar os dados do pedido
                RegistraVendaFinalizada(usuarioId);
                return RedirectToAction("HomePageCliente", "HomeCliente");
            }

            // Retorna a view com mensagens de validação
            return NotFound();
        }

        // Após finalizar a venda, o carrinho é limpo e a compra é registrada no histórico
        public void RegistraVendaFinalizada(int usuarioId)
        {
            var livrosNoCarrinho = _db.Carrinhos.Where(c => c.IdUsuario ==  usuarioId).ToList();
            var usuario = _db.Usuarios.FirstOrDefault(u => u.IdUsuario == usuarioId);

            var novoRegistro = new RegistroDeVendas
            {
                IdUsuario = usuarioId,
                NomeUsuario = usuario.Nome,
                Data = DateTime.Now,
                Total = livrosNoCarrinho.Sum(l => l.Subtotal)
            };

            _db.RegistroDeVendas.Add(novoRegistro);
            _db.SaveChanges();

            foreach (var livro in livrosNoCarrinho)
            {
                var livroEmDestaque = _db.Livros.FirstOrDefault(l => l.LivroId == livro.LivroId);

                var detalhesNovoRegistro = new DetalhesVenda
                {
                    IdRegistroVenda = novoRegistro.IdCompra,
                    LivroId = livro.LivroId,
                    Titulo = livroEmDestaque.Titulo,
                    CapaLivro = livroEmDestaque.CapaLivro,
                    ValorUnit = livroEmDestaque.ValorVenda,
                    Quantidade = livro.Quantidade
                };

                _db.DetalhesVendas.Add(detalhesNovoRegistro);
                _db.SaveChanges();
            }

            AtualizaEstoque(livrosNoCarrinho);
            _db.Carrinhos.RemoveRange(livrosNoCarrinho);
            _db.SaveChanges();
        }

        // Métodos que retornam compras finalizadas
        public ActionResult MostrarRegistroSimples()
        {
            var compras = _db.RegistroDeVendas.Select(compra => new RegistroDeVendaVM
            {
                IdCompra = compra.IdCompra,
                IdUsuario = compra.IdUsuario,
                NomeUsuario = compra.NomeUsuario,
                Data = compra.Data,
                Total = compra.Total,
            }).ToList();

            return View(compras);
        }

        public ActionResult MostrarRegistroCompleto(int idRegistroDeVenda)
        {
            var registro = _db.RegistroDeVendas.FirstOrDefault(r => r.IdCompra == idRegistroDeVenda);

            var itens = _db.DetalhesVendas.Where(i => i.IdRegistroVenda == idRegistroDeVenda).ToList();

            var viewModel = new RegistroDeVendaCompletoVM
            {
                RegistroDeVenda = registro,
                ItensVenda = itens
            };

            return View(viewModel);
        }

        public void AtualizaEstoque(List<Carrinho> compraFinalizada)
        {
            foreach (var livro in compraFinalizada)
            {
                var livroEmDestaque = _db.Livros.FirstOrDefault(l => l.LivroId == livro.LivroId);
                livroEmDestaque.QntdEstoque -= livro.Quantidade;
                _db.SaveChanges();
            }
        }
    }
}