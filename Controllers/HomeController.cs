using ITicket.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SQLitePCL;
using System.Linq;
using System.Security.Claims;

namespace ITicket.Controllers {
    public class HomeController : Controller
    {
        private readonly ContextoDb _contexto;

        public HomeController(ContextoDb contexto)
        {
            _contexto = contexto;
        }

        public IActionResult MinhaConta()
{
        // resgata o nome do usuario da sessao atual
        var username = HttpContext.Session.GetString("Username");

        // procura o usuario no db
        var usuario = _contexto.Usuario.FirstOrDefault(u => u.Username == username);

        // caso nao seja encontrado, redireciona para pag de login
        if (usuario == null)
        {
            return RedirectToAction("Login", "Account");
        }

        // passa o usuario para a view
        return View(usuario);
    }
        
       public IActionResult Index(int page = 1, string sort = null, string filtro = null, string search = null) {
    var username = HttpContext.Session.GetString("Username");          
    if(username == null){
        return View("Login");
    }

    int pageSize = 10;
    var chamados = _contexto.ChamadoView.AsQueryable();

    // aplica os filtros se especificados
    if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrEmpty(search)) {
        switch (filtro) {
        case "IdChamado":
            chamados = chamados.Where(c => c.IdChamado.ToString().Contains(search));
            break;
        case "Abertura":
            chamados = chamados.Where(c => c.Abertura.HasValue && c.Abertura.Value.ToString().Contains(search));
            break;
        case "Solicitante":
            chamados = chamados.Where(c => c.Solicitante.Contains(search));
            break;
        case "Status":
            chamados = chamados.Where(c => c.Status.Contains(search));
            break;
        case "HoraLimite":
        DateTime searchDate;
        if (DateTime.TryParse(search, out searchDate)) {
            chamados = chamados.Where(c => c.HoraLimite.HasValue && c.HoraLimite.Value.Date == searchDate.Date);
        }
        break;
        }
    }

    // sort toda vez que o titulo da coluna é clicado
    if (!string.IsNullOrEmpty(sort)) {
        switch (sort) {
        case "IdChamado":
            chamados = chamados.OrderBy(c => c.IdChamado);
            break;
        case "Abertura":
            chamados = chamados.OrderBy(c => c.Abertura);
            break;
        case "Solicitante":
            chamados = chamados.OrderBy(c => c.Solicitante);
            break;
        case "Status":
            chamados = chamados.OrderBy(c => c.Status);
            break;
        case "HoraLimite":
            chamados = chamados.OrderBy(c => c.HoraLimite);
            break;
       
        }
    }

    // Apply pagination
    chamados = chamados
        .Skip((page - 1) * pageSize)
        .Take(pageSize);

    ViewBag.Page = page;
    return View(chamados.ToList());
}

       
        public IActionResult Administracao()
        {
            return View();
        }
        
        public IActionResult Login() {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
