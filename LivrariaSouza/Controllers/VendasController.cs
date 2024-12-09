using LivrariaSouza.DataAccess;
using LivrariaSouza.Models.Models;
using LivrariaSouza.Models.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaSouza.Controllers
{
    public class VendasController : Controller
    {
        private readonly AppDbContext _db;

        public VendasController(AppDbContext db)
        {
            _db = db;
        }

        public void RegistraVendaFinalizada(Carrinho carrinho)
        {
            var usuario = _db.Usuarios.FirstOrDefault(u => u.IdUsuario == carrinho.IdUsuario);

            var novoRegistro = new RegistroDeVendas
            {
                IdUsuario = carrinho.IdUsuario,
                NomeUsuario = usuario.Nome,
                Data = DateTime.Now,
            };
        }

        // Métodos que retornam compras finalizadas
        public ActionResult MostrarRegistroSimples()
        {
            var compras = _db.RegistroDeVendas.Select(compra => new RegistroDeVendaVM
            {
                IdCompra = compra.IdCompra,
                IdUsuario = compra.IdUsuario,
                NomeUsuario = compra.NomeUsuario,
                Data = compra.Data,
                Total = compra.Total,
            }).ToList();

            return View(compras);
        }

        public ActionResult MostrarRegistroCompleto(int idRegistroDeVenda)
        {
            var detalhes = _db.RegistroDeVendas.Where(r => r.IdCompra == idRegistroDeVenda).ToList();

            return View(detalhes);
        }
    }
}
