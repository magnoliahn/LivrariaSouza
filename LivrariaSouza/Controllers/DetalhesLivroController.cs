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

        public IActionResult CarregarFormulario(int Id) // Carrega página para editar os detalhes do livro
        {
            var livro = _db.Livros.FirstOrDefault(l => l.Id == Id);
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Essa linha é importante para segurança
        public IActionResult EditarDetalhesLivro(Livro livroEditado)
        {
            if (!ModelState.IsValid) // Verifica se o modelo é válido
            {
                // Retorna à view com erros
                return View("CarregarFormulario", livroEditado);
            }

            var livro = _db.Livros.FirstOrDefault(l => l.Id == livroEditado.Id);
            if (livro == null)
            {
                return NotFound();
            }

            // Atualiza as propriedades do livro
            livro.Titulo = livroEditado.Titulo;
            livro.Imagem = livroEditado.Imagem;
            livro.Autor = livroEditado.Autor;
            livro.NumeroPag = livroEditado.NumeroPag;
            livro.ValorVenda = livroEditado.ValorVenda;
            livro.ValorCompra = livroEditado.ValorCompra;
            livro.AnoLancamento = livroEditado.AnoLancamento;
            livro.QntdEstoque = livroEditado.QntdEstoque;
            livro.Descricao = livroEditado.Descricao;

            // Salva as mudanças no banco de dados
            _db.SaveChanges();

            return RedirectToAction("MostraDetalhesLivro", new { id = livro.Id });
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

            TempData["MensagemLivroDeletado"] = "Livro deletado com sucesso!";
            TempData["TipoMensagem"] = "success"; // Alerta de sucesso

            return RedirectToAction("RetornaTodosLivros", "Home"); // Redireciona para a página principal ou outra de sua escolha
        }
    }
}