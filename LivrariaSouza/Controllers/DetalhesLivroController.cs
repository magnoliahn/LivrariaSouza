using LivrariaSouza.DataAccess;
using LivrariaSouza.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaSouza.Controllers
{
    public class DetalhesLivroController : Controller
    {
        private readonly AppDbContext _db;
        public DetalhesLivroController(AppDbContext db)
        {
            _db = db;
        }

        [Route("/admin/DetalhesLivro")]
        public IActionResult MostraDetalhesLivro(int id)
        {
            var livro = _db.Livros.FirstOrDefault(l => l.LivroId == id);
            if (livro == null)
            {
                return View("NotFound");
            }
            return View(livro);
        }

        [Route("/admin/EditarLivro")]
        public IActionResult CarregarFormulario(int Id)
        {
            var livro = _db.Livros.FirstOrDefault(l => l.LivroId == Id);
            if (livro == null)
            {
                return View("NotFound");
            }
            return View(livro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/EditarLivro")]
        public IActionResult EditarDetalhesLivro(Livro livroEditado)
        {
            if (!ModelState.IsValid)
            {
                return View("CarregarFormulario", livroEditado);
            }

            var livro = _db.Livros.FirstOrDefault(l => l.LivroId == livroEditado.LivroId);
            if (livro == null)
            {
                return View("NotFound");
            }

            livro.Titulo = livroEditado.Titulo;
            livro.Imagem = livroEditado.Imagem;
            livro.Autor = livroEditado.Autor;
            livro.NumeroPag = livroEditado.NumeroPag;
            livro.ValorVenda = livroEditado.ValorVenda;
            livro.ValorCompra = livroEditado.ValorCompra;
            livro.AnoLancamento = livroEditado.AnoLancamento;
            livro.QntdEstoque = livroEditado.QntdEstoque;
            livro.Descricao = livroEditado.Descricao;

            _db.SaveChanges();

            return RedirectToAction("MostraDetalhesLivro", new { id = livro.LivroId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/DeletarLivro")]
        public IActionResult DeletaLivro(int id)
        {
            var livro = _db.Livros.FirstOrDefault(l => l.LivroId == id);

            if (livro == null)
            {
                return View("NotFound");
            }

            _db.Livros.Remove(livro);
            _db.SaveChanges();

            TempData["MensagemLivroDeletado"] = "Livro deletado com sucesso!";
            TempData["TipoMensagem"] = "success";

            return RedirectToAction("RetornaTodosLivros", "Home");
        }
    }
}