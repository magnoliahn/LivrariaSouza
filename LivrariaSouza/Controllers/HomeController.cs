using LivrariaSouza.DataAccess;
using LivrariaSouza.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.RegularExpressions;

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

    public IActionResult CriarLivro()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CriarLivro(Livro livro)
    {
        // Tente converter o valor de string para decimal
        if (!decimal.TryParse(livro.ValorString.Replace(".", ","), NumberStyles.Any, new CultureInfo("pt-BR"), out decimal valorConvertido))
        {
            ModelState.AddModelError("ValorString", "O valor deve conter somente números e pode incluir uma vírgula ou ponto para os centavos.");
        }
        else if (valorConvertido < 0)
        {
            ModelState.AddModelError("ValorString", "O valor não pode ser negativo.");
        }
        else
        {
            livro.Valor = valorConvertido; // Atribui o valor convertido à propriedade real
        }

        if (livro.QntdEstoque < 0)
        {
            ViewData["MensagemEstoqueNegativo"] = "O estoque não pode ser negativo.";
            ViewData["TipoMensagem"] = "danger"; // Usando 'danger' para um alerta vermelho
            return View(livro);
        }

        if (ModelState.IsValid)
        {
            _db.Livros.Add(livro);
            _db.SaveChanges();
            TempData["MensagemLivroAdicionado"] = "Livro adicionado com sucesso!";
            TempData["TipoMensagem"] = "success"; // Alerta de sucesso

            return RedirectToAction("RetornaTodosLivros");
        }
        return View(livro);
    }
}
