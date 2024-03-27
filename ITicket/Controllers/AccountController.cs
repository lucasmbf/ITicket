using Microsoft.AspNetCore.Mvc;
using ITicket.Models;

namespace ITicket.Controllers {
    public class AccountController : Controller {
        public IActionResult Login(LoginViewModel model) {
            if (model.Username == "admin" && model.Password == "admin") {
                // Usuário autenticado com sucesso
                // Pode ser feita uma lógica para armazenar o estado de autenticação, como em um cookie ou na sessão
                return RedirectToAction("Index", "Home");
            }

            // Login inválido, redireciona de volta para a página de login
            return RedirectToAction("Login", "Account");
        }
    }

}
