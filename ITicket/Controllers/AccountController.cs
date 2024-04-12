using Microsoft.AspNetCore.Mvc;
using ITicket.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ITicket.Controllers {
    public class AccountController : Controller {
        private readonly ContextoDb _contexto;
        private readonly ILogger<AccountController> _logger; //Adicionado para logar as ações do usuário

        public AccountController(ContextoDb contexto, ILogger<AccountController> logger) {
            _contexto = contexto;
            _logger = logger;
        }

       [HttpPost]
        public IActionResult ValidateLogin(string Username, string Senha) {
            var usuario = _contexto.Usuario.FirstOrDefault(u => u.Username == Username && u.Senha == Senha);

            if (usuario != null)
            {
                _logger.LogInformation("Starting session for user {Username}", usuario.Username);
                HttpContext.Session.SetString("Username", usuario.Username);
                _logger.LogInformation("Session started for user {Username}", usuario.Username);
                return RedirectToAction("Index", "Home");
            } else{
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida");
                return RedirectToAction("Login");
            }
    
}

        //Metodo para testar a conexao com o banco de dados pela url https://localhost:7243/account/testeconexao 
        public IActionResult TesteConexao() 
{
    try 
    {
        var usuario = _contexto.Usuario.FirstOrDefault(u => u.IdUsuario == 1);

        if (usuario != null) 
        {
            return Content($"ID: {usuario.IdUsuario}, Nome: {usuario.Nome}, Email: {usuario.Email}, Cargo: {usuario.Cargo}, Username: {usuario.Username}, Senha: {usuario.Senha}");
        }

        return Content("Usuário não encontrado.");
    } 
    catch (Exception ex) 
    {
        return BadRequest($"An error occurred: {ex.Message}");
    }
}

        public IActionResult TestConnection()
{
    try
    {
        _contexto.Database.OpenConnection();
        _contexto.Database.CloseConnection();
        return Ok("Connection to database successful");
    }
    catch (Exception ex)
    {
        return BadRequest($"Connection to database failed: {ex.Message}");
    }
}
    }
}