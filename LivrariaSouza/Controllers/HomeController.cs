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
        if (livro.Titulo.Length > 100)
        {
            ModelState.AddModelError("Titulo","O Titulo do livro n�o pode conter mais de 100 caracteres.");
        }
        // Tente converter o valor de string para decimal
        if (!decimal.TryParse(livro.ValorString.Replace(".", ","), NumberStyles.Any, new CultureInfo("pt-BR"), out decimal valorConvertido))
        {
            ModelState.AddModelError("ValorString", "O valor deve conter somente n�meros e pode incluir uma v�rgula ou ponto para os centavos.");
        }
        else if (valorConvertido < 0)
        {
            ModelState.AddModelError("ValorString", "O valor n�o pode ser negativo.");
        }
        else
        {
            livro.Valor = valorConvertido; // Atribui o valor convertido � propriedade real
        }
        // mesma l�gica para o pre�o de custo
        if (!decimal.TryParse(livro.ValorString.Replace(".", ","), NumberStyles.Any, new CultureInfo("pt-BR"), out decimal custoConvertido))
        {
            ModelState.AddModelError("ValorString", "O valor deve conter somente n�meros e pode incluir uma v�rgula ou ponto para os centavos.");
        }
        else if (custoConvertido < 0)
        {
            ModelState.AddModelError("ValorString", "O valor n�o pode ser negativo.");
        }
        else
        {
            livro.Custo = custoConvertido; // Atribui o valor convertido � propriedade real
        }

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
