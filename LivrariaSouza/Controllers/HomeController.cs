using LivrariaSouza.DataAccess;
using LivrariaSouza.DataAccess.Repository.Services;
using LivrariaSouza.Models.Models;
using Microsoft.AspNetCore.Mvc;

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
        return RedirectToAction("HomePageCliente", "HomeCliente");
    }
    [Route("/admin/LivrariaSouza")]
    public IActionResult RetornaTodosLivros()
    {
        var livros = _db.Livros.ToList();
        return View(livros);
    }

    [HttpGet]
    [Route("/admin/CriarLivro")]
    public IActionResult CriarLivro()
    {
        return View();
    }

    [HttpPost]
    [Route("/admin/CriarLivro")]
    public IActionResult CriarLivro(Livro livro)
    {
        ModelState.Clear();
        // Valida se título tem no max 100 caracteres
        if (livro.Titulo.Length > 100)
        {
            ModelState.AddModelError("Titulo", "O Titulo do livro não pode conter mais de 100 caracteres.");
        }

        var valorVenda = _valoresServices.ValidarCaracteres(livro.ValorVendaString);

        // Valida se valor escolhido foi digitado corretamente
        if (valorVenda is string stringValorVenda)
        {
            ModelState.AddModelError("ValorVendaString", stringValorVenda);
        }
        else if (valorVenda is decimal decimalValorVenda)
        {
            livro.ValorVenda = decimalValorVenda;
        }

        // Mesma lógica para o valor de compra.
        var valorCompra = _valoresServices.ValidarCaracteres(livro.ValorCompraString);

        if (valorCompra is string stringValorCompra)
        {
            ModelState.AddModelError("ValorCompraString", stringValorCompra);
        }
        else if (valorCompra is decimal decimalValorCompra)
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("/admin/DeletarLivros")]
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

        TempData["MensagemLivroDeletado"] = "Livros excluídos com sucesso.";
        TempData["TipoMensagem"] = "success";

        return RedirectToAction("RetornaTodosLivros"); // Redireciona para a lista de livros
    }

    public IActionResult Error404()
    {
        Response.StatusCode = 404; // Define o código de status para 404
        return View("HomePageCliente"); // Retorna a view que você deseja exibir
    }

}
