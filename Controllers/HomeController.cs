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
        
        public IActionResult Index() {
        var username = HttpContext.Session.GetString("Username");          
         if(username == null){
             return View("Login");
         }

        var servicos = _contexto.Servico.ToList();
        return View(servicos);
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
