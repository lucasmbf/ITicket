using Microsoft.AspNetCore.Mvc;
using ITicket.Models; 


public class AdministracaoController : Controller
{
    private readonly ContextoDb _contexto;

    public AdministracaoController(ContextoDb context)
    {
        _contexto = context;
    }


    //metodo para cadastrar usuario
    //method to register new user
    [HttpPost]
    public IActionResult Create(Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            _contexto.Add(usuario);
            _contexto.SaveChanges();

            ViewBag.Message = "Usuario Cadastrado com Sucesso!";

            return View("~/Views/Home/Administracao.cshtml", usuario);
        }

        return View("~/Views/Home/Administracao.cshtml", usuario);
    }

}