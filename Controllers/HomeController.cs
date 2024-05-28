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
        
public IActionResult Index(int page = 1, string sort = null, string filtro = null, string valor = null) 
{
    var username = HttpContext.Session.GetString("Username");          
    if(username == null)
    {
        return View("Login");
    }

    int pageSize = 10;
    var chamados = _contexto.ChamadoView.AsQueryable();
    ViewBag.Filtro = filtro;
    ViewBag.Valor = valor;

    if (filtro == "Todos")
    {
        chamados = chamados.Where(c => c.Abertura >= DateTime.Now.AddDays(-365));
    }
    else if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrEmpty(valor)) 
    {
        switch (filtro) 
        {
            case "IdChamado":
            if (int.TryParse(valor, out int idChamado))
            {
                chamados = chamados.Where(c => c.IdChamado == idChamado);
            }
            // Handle invalid input
            else
            {
                // Log error, show error message, etc.
            }
            break;
        case "Abertura":
            if (DateTime.TryParse(valor, out DateTime abertura))
            {
                chamados = chamados.Where(c => c.Abertura.HasValue && c.Abertura.Value.Date == abertura.Date);
            }
            // Handle invalid input
            else
            {
                // Log error, show error message, etc.
            }
            break;
        case "Solicitante":
            chamados = chamados.Where(c => c.Solicitante.ToLower().Contains(valor.ToLower()));
            break;
        case "Status":
            chamados = chamados.Where(c => c.Status.ToLower().Contains(valor.ToLower()));
            break;
        case "HoraLimite":
            if (DateTime.TryParse(valor, out DateTime horaLimite))
            {
                chamados = chamados.Where(c => c.HoraLimite.HasValue && c.HoraLimite.Value.Date == horaLimite.Date);
            }
            // Handle invalid input
            else
            {
                // Log error, show error message, etc.
            }
            break;
        }
    }

    // Sort every time the column title is clicked
    if (!string.IsNullOrEmpty(sort)) 
    {
        switch (sort) 
        {
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
