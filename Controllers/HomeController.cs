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
        
        public IActionResult Index(int page = 1) {
        var username = HttpContext.Session.GetString("Username");          
         if(username == null){
             return View("Login");
         }

        
        int pageSize = 10;
        var chamados = _contexto.ChamadoView
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
            ViewBag.Page = page;
            return View(chamados);
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
