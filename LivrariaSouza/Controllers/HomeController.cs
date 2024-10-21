using LivrariaSouza.DataAccess;
using LivrariaSouza.DataAccess.Repository.Services;
using LivrariaSouza.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

public class HomeController : Controller
{
    private readonly AppDbContext _db;
    private readonly ValoresServices _valoresServices;

    public HomeController(AppDbContext db, ValoresServices valoresServices)
    {
        _db = db;
        _valoresServices = valoresServices;
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
        // Valida se título tem no max 100 caracteres
        if (livro.Titulo.Length > 100)
        {
            ModelState.AddModelError("Titulo","O Titulo do livro não pode conter mais de 100 caracteres.");
        }

        // Valida se valor escolhido foi digitado corretamente
        if (_valoresServices.ValidarCaracteres(livro.ValorVendaString) is string stringValorVenda)
        {
            ModelState.AddModelError("ValorVendaString", stringValorVenda);
        }
        else if(_valoresServices.ValidarCaracteres(livro.ValorVendaString) is decimal decimalValorVenda)
        {
            livro.ValorVenda = decimalValorVenda;
        }

        // Mesma lógica para o valor de compra.
        if (_valoresServices.ValidarCaracteres(livro.ValorCompraString) is string stringValorCompra)
        {
            ModelState.AddModelError("ValorCompraString", stringValorCompra);
        }
        else if (_valoresServices.ValidarCaracteres(livro.ValorCompraString) is decimal decimalValorCompra)
        {
            livro.ValorCompra = decimalValorCompra;
        }

        // Verifica se estoque é negativo
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
