using Microsoft.AspNetCore.Mvc;
using ITicket.Models;
using Microsoft.EntityFrameworkCore;

namespace ITicket.Controllers {
    public class ITicketController : Controller {

        private readonly ContextoDb _contexto;

        public ITicketController(ContextoDb contexto)
        {
            _contexto = contexto;
        }
        public async Task<IActionResult> Index() {
            return View(await _contexto.Chamado.ToListAsync());
        }

        
    }
}
