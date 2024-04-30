using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ITicket.Models;
using ITicket.Controllers;

public class ChamadoController : Controller
{
    private readonly ContextoDb _contexto; 

    public ChamadoController(ContextoDb contexto) 
    {
        _contexto = contexto; 
    }
    
    [HttpGet]
    public IActionResult GetPriorityForDescription(string descricao)
    {
        // Assuming _context is your DbContext
        var servico = _contexto.Servico.FirstOrDefault(s => s.Descricao == descricao);

        if (servico == null)
        {
            return NotFound();
        }

        return Ok(servico.Prioridade);
    }
    public IActionResult AbrirChamado()
    {        
        var chamados = _contexto.Chamado.ToList();
        var servicos = _contexto.Servico.Select(s => s.Descricao).ToList();

        var username = HttpContext.Session.GetString("Username"); 
        var usuario = _contexto.Usuario.FirstOrDefault(u => u.Username == username); 

        if (usuario == null)
    {
        
        return RedirectToAction("Login", "Account");
    }
        ViewData["Servicos"] = servicos;
        ViewData["Usuario"] = usuario; 

        return View("~/Views/Home/AbrirChamado.cshtml", chamados);
    }

    [HttpPost]
    public IActionResult AbrirChamado(Chamado chamado, string Massivo)
    {
        if (ModelState.IsValid)
        {
            if (Massivo == "Sim")
            {
                chamado.Prioridade = "Alta";
            }

            _contexto.Chamado.Add(chamado);
            _contexto.SaveChanges();

            return RedirectToAction("Index");
        }

        return View("~/Views/Home/AbrirChamado.cshtml", chamado);
    }
}