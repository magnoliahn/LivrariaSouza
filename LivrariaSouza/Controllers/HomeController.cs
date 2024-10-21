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
        // Valida se t�tulo tem no max 100 caracteres
        if (livro.Titulo.Length > 100)
        {
            ModelState.AddModelError("Titulo","O Titulo do livro n�o pode conter mais de 100 caracteres.");
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

        // Mesma l�gica para o valor de compra.
        if (_valoresServices.ValidarCaracteres(livro.ValorCompraString) is string stringValorCompra)
        {
            ModelState.AddModelError("ValorCompraString", stringValorCompra);
        }
        else if (_valoresServices.ValidarCaracteres(livro.ValorCompraString) is decimal decimalValorCompra)
        {
            livro.ValorCompra = decimalValorCompra;
        }

        // Verifica se estoque � negativo
        if (livro.QntdEstoque < 0)
        {
            ViewData["MensagemEstoqueNegativo"] = "O estoque n�o pode ser negativo.";
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletaLivrosEmBloco(int[] livroIds)
    {
        if (livroIds == null || livroIds.Length == 0)
        {
            TempData["MensagemLivroDeletado"] = "Nenhum livro selecionado para excluir.";
            TempData["TipoMensagem"] = "error";
            return RedirectToAction("RetornaTodosLivros"); // Redireciona para a lista de livros
        }

        var livrosSelecionados = _db.Livros.Where(l => livroIds.Contains(l.Id)).ToList();
        if (!livrosSelecionados.Any())
        {
            TempData["MensagemLivroDeletado"] = "Nenhum livro encontrado para excluir.";
            TempData["TipoMensagem"] = "error";
            return RedirectToAction("RetornaTodosLivros"); // Redireciona para a lista de livros
        }

        _db.Livros.RemoveRange(livrosSelecionados);
        _db.SaveChanges();

        TempData["MensagemLivroDeletado"] = "Livros exclu�dos com sucesso.";
        TempData["TipoMensagem"] = "success";

        return RedirectToAction("RetornaTodosLivros"); // Redireciona para a lista de livros
    }
}
