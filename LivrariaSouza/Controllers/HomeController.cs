using LivrariaSouza.DataAccess;
using LivrariaSouza.Models.Models;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        return RedirectToAction("RetornaTodosLivros");
    }

    public IActionResult RetornaTodosLivros()
    {
        var livros = _db.Livros.ToList();
        return View(livros);
    }

    public IActionResult DetalhesLivro(int id)
    {
        var livro = _db.Livros.FirstOrDefault(livro => livro.Id == id);
        if (livro == null)
        {
            return NotFound();
        }
        return View(livro);
    }
    public IActionResult CriarLivro()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CriarLivro(Livro livro)
    {
        if (ModelState.IsValid)
        {
            _db.Livros.Add(livro);
            _db.SaveChanges();
            return RedirectToAction("RetornaTodosLivros");
        }
        return View(livro);
    }
}
