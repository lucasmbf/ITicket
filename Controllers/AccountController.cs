﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ITicket.Models;
using System.Linq;

namespace ITicket.Controllers
{
    public class AccountController : Controller
    {
        private readonly ContextoDb _contexto;
        private readonly ILogger<AccountController> _logger; //Adicionado para logar as ações do usuário
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(ContextoDb contexto, ILogger<AccountController> logger, SignInManager<IdentityUser> signInManager)
        {
            _contexto = contexto;
            _logger = logger;
            _signInManager = signInManager;
        }



        //metodo de login
        //Login Method
        [HttpPost]
        public IActionResult ValidateLogin(string Username, string Senha)
        {
            var usuario = _contexto.Usuario.FirstOrDefault(u => u.Username == Username && u.Senha == Senha);

            if (usuario != null)
            {
                _logger.LogInformation("Starting session for user {Username}", usuario.Username);
                HttpContext.Session.SetString("Username", usuario.Username);
                _logger.LogInformation("Session started for user {Username}", usuario.Username);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha incorreta, tente novamente.");
                return View("Login");
            }
        }

        //recupera o nome do usuário logado
        //retrieves property Nome(Name) of currently logged in user
        [HttpGet]
        public IActionResult GetUserName()
        {
            var username = HttpContext.Session.GetString("Username");
            Console.WriteLine("Username: " + username); // registra username
            if (username != null)
            {
                var user = _contexto.Usuario.FirstOrDefault(u => u.Username == username);
                Console.WriteLine("User: " + user); // registra o objeto usuario
                if (user != null)
                {
                    var userInfo = new
                    {
                        Nome = user.Nome,
                        Departamento = user.Departamento,
                        Cargo = user.Cargo
                    };
                    return Json(userInfo);
                }
            }
            return Json(new { Nome = "", Departamento = "", Cargo = "" });
        }


        [HttpGet]
        public IActionResult IsSignedIn()
        {
            return Json(_signInManager.IsSignedIn(User));
        }

        //Metodo para testar a conexao com o banco de dados pela url https://localhost:7243/account/testeconexao  - deve ser removido em prod 
        //tests the database connection - must be removed in production        

        public IActionResult TestConnection()
        {
            try
            {
                if (_contexto.Database.CanConnect())
                {
                    return Ok("Connection to database successful");
                }
                else
                {
                    return BadRequest("Connection to database failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Connection to database failed: {ex.Message}");
            }
        }
    }
}