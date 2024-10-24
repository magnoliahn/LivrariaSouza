using LivrariaSouza.DataAccess;
using LivrariaSouza.DataAccess.Repository.Services;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaSouza.Controllers
{
    public class HomeClienteController : Controller
    {
        private readonly AppDbContext _db;

        public HomeClienteController(AppDbContext db)
        {
            _db = db;
        }

        [Route("/cliente/LivrariaSouza")]
        public IActionResult HomePageCliente()
        {
            var livros = _db.Livros.ToList();
            return View("~/Views/Home/HomePageCliente.cshtml", livros);
        }

        [Route("/cliente/DetalhesLivro")]
        public IActionResult MostraDetalhesLivroCliente(int id)
        {
            var livro = _db.Livros.Find(id);
            if (livro == null)
            {
                return NotFound();
            }
            return View("~/Views/Home/MostraDetalhesLivroCliente.cshtml", livro);
        }
    }
}
