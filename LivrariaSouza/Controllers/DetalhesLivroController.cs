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

        public IActionResult MostraDetalhesLivro(int id)
        {
            var livro = _db.Livros.FirstOrDefault(l => l.Id == id);
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        public IActionResult EditarLivro(int id) // Carrega página para editar os detalhes do livro
        {
            var livro = _db.Livros.FirstOrDefault(l => l.Id == id);
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        [HttpPost]
        public IActionResult EditarDetalhesLivro(int Id, Livro livroEditado)
        {
            var livro = _db.Livros.FirstOrDefault(l => l.Id == Id);

            if (livro == null)
            {
                return NotFound();
            }

            livro.Titulo = livroEditado.Titulo;
            livro.Imagem = livroEditado.Imagem;
            livro.Autor = livroEditado.Autor;
            livro.NumeroPag = livroEditado.NumeroPag;
            livro.Valor = livroEditado.Valor;
            livro.AnoLancamento = livroEditado.AnoLancamento;
            livro.QntdEstoque = livroEditado.QntdEstoque;
            livro.Descricao = livroEditado.Descricao;

            _db.SaveChanges();

            return RedirectToAction("MostraDetalhesLivro", new { id = Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletaLivro(int id)
        {
            var livro = _db.Livros.FirstOrDefault(l => l.Id == id);

            if (livro == null)
            {
                return NotFound();
            }

            _db.Livros.Remove(livro);
            _db.SaveChanges();

            return RedirectToAction("RetornaTodosLivros", "Home"); // Redireciona para a página principal ou outra de sua escolha
        }
    }
}