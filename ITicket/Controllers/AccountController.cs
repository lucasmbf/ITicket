using Microsoft.AspNetCore.Mvc;
using ITicket.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ITicket.Controllers {
    public class AccountController(ContextoDb context) : Controller {
        private readonly ContextoDb _contexto = context;

        public IActionResult Login(Usuario model) {
            var user = _contexto.Usuario.FirstOrDefault(u => u.Username == model.Username && u.Senha == model.Senha);

            if (user != null) {
                // Usuário autenticado com sucesso
                // Pode ser feita uma lógica para armazenar o estado de autenticação, como em um cookie ou na sessão
                return RedirectToAction("Index", "Home");
            }
            // Login inválido, redireciona de volta para a página de login
            return RedirectToAction("Login", "Account");
        }

    }
}
