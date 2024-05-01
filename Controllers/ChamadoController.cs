using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ITicket.Models; 
using System.ComponentModel;

public class ChamadoController : Controller 
{
    private readonly ContextoDb _contexto;
    
    public ChamadoController(ContextoDb context) 
    {
        _contexto = context;
    }


    [HttpGet]
    public IActionResult GetPriorityForDescription(string descricao)
    {
        var servico = _contexto.Servico.FirstOrDefault(s => s.Descricao == descricao);
        if (servico != null)
        {
            return Ok(servico.Prioridade);
        }
        else
        {
            return NotFound();
        }
    }
    public IActionResult AbrirChamado()
    {
        var loggedInUsername = HttpContext.Session.GetString("Username");
        var user = _contexto.Usuario.FirstOrDefault(u => u.Username == loggedInUsername);
        var servicos = _contexto.Servico.Select(s => s.Descricao).ToList();

        var viewModel = new ChamadoUsuarioViewModel
        {
            Usuario = new Usuario
            {
                Nome = user.Nome,
                Departamento = user.Departamento,
                Cargo = user.Cargo,
            },
            Servico = servicos // Add the Descricao values to the view model
        };

        return View("~/Views/Home/AbrirChamado.cshtml", viewModel);
    }

    [HttpPost]
    public IActionResult AbrirChamado(Chamado chamado)
    {
        if(ModelState.IsValid)
        {
        
        chamado.Abertura = DateTime.Now;
        var servico = _contexto.Servico.FirstOrDefault(s => s.Descricao == chamado.Servico.Descricao);
        chamado.IdServico = servico != null ? (int)servico.IdServico : 0;
        
        
        bool isMassivo = false;
        bool.TryParse(chamado.Massivo, out isMassivo);
        if (isMassivo)
        {
            if (servico.Prioridade == "Baixa")
            {
                chamado.Prioridade = "Media";
            }
            else
            {
                chamado.Prioridade = "Alta";
            }
        }
        else
        {
            chamado.Prioridade = servico.Prioridade;
        }    
            System.Diagnostics.Debug.WriteLine($"Prioridade: {chamado.Prioridade}");
            _contexto.Add(chamado);
            _contexto.SaveChanges();

            ViewBag.Message = "Chamado Aberto com Sucesso!";

            var username = HttpContext.Session.GetString("Username");
            var usuario = _contexto.Usuario.FirstOrDefault(u => u.Username == username); // Replace this with your method for getting the Usuario object

            if (usuario == null)
            {
                ModelState.AddModelError("", "The user does not exist.");
                return View("~/Views/Home/Error.cshtml");
            }

            var viewModel = new ChamadoUsuarioViewModel
            {
                Chamado = chamado,
                Usuario = usuario
            };

            return View("~/Views/Home/AbrirChamado.cshtml", viewModel);
        }
        else
        {
            ModelState.AddModelError("", "There was an error opening the ticket. Please try again.");

        var username = HttpContext.Session.GetString("Username");
        var usuario = _contexto.Usuario.FirstOrDefault(u => u.Username == username);

        var viewModel = new ChamadoUsuarioViewModel
        {
            Chamado = chamado,
            Usuario = usuario
        };

        return View("~/Views/Home/AbrirChamado.cshtml", viewModel);
        }
    }
}