using Microsoft.AspNetCore.Mvc;
using ITicket.Models;
using Microsoft.EntityFrameworkCore;

namespace ITicket.Controllers {
    public class ITicketController : Controller {

        private readonly Contexto _contexto;

        public ITicketController(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<IActionResult> Index() {
            return View(await _contexto.Chamado.ToListAsync());
        }

        
    }
}
