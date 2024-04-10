using Microsoft.AspNetCore.Mvc;
using ITicket.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ITicket.Controllers {
    public class AccountController : Controller {
        private readonly ContextoDb _contexto;

        public AccountController(ContextoDb contexto) {
            _contexto = contexto;
        }

        public IActionResult ValidateLogin(Usuario usuario) {
            var user = _contexto.Usuario.FirstOrDefault(u => u.Username == usuario.Username && u.Senha == usuario.Senha);

            if (user != null) {
                return RedirectToAction("Index", "Home");
            }

            ViewData["MensagemErro"] = "Credenciais inválidas. Tente novamente.";
            return View("~/Views/Home/Index.cshtml");
        }




        //Metodo para testar a conexao com o banco de dados pela url https://localhost:7243/account/testeconexao 
        public IActionResult TesteConexao() {
            var usuario = _contexto.Usuario.FirstOrDefault(u => u.IdUsuario == 1);

            if (usuario != null) {
                return Content($"ID: {usuario.IdUsuario}, Nome: {usuario.Nome}, Email: {usuario.Email}");
            }

            return Content("Usuário não encontrado.");
        }



    }
}


