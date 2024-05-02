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

            if(chamado.Servico != null)
            {
                var servico = _contexto.Servico.FirstOrDefault(s => s.Descricao == chamado.Servico.Descricao);

                if(servico != null)
                {
                    chamado.IdServico = (int)servico.IdServico;

                    bool isMassivo = false;
                    bool.TryParse(chamado.Massivo, out isMassivo);
                    if (isMassivo)
                    {
                        chamado.Prioridade = servico.Prioridade == "Baixa" ? "Media" : "Alta";
                    }
                    else
                    {
                        chamado.Prioridade = servico.Prioridade;
                    }
                }
                else
                {
                    ModelState.AddModelError("", "No matching service found.");
                    return View("~/Views/Home/Error.cshtml");
                }
            }
            else
            {
                ModelState.AddModelError("", "Service is null.");
                return View("~/Views/Home/Error.cshtml");
            }

            System.Diagnostics.Debug.WriteLine($"Prioridade: {chamado.Prioridade}");
            _contexto.Add(chamado);
            _contexto.SaveChanges();

            ViewBag.Message = "Chamado Aberto com Sucesso!";

            var username = HttpContext.Session.GetString("Username");
            var usuario = _contexto.Usuario.FirstOrDefault(u => u.Username == username);

            if (usuario == null)
            {
                ModelState.AddModelError("", "The user does not exist.");
                return View("~/Views/Home/Error.cshtml");
            }

            var viewModel = new ChamadoUsuarioViewModel
{
    Chamado = chamado,
    Usuario = usuario,
    Servico = _contexto.Servico.Select(s => s.Descricao).ToList() // Fetch all Servico descriptions from your database
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
                Usuario = usuario,
                Servico = _contexto.Servico.Select(s => s.Descricao).ToList() // Fetch all Servico descriptions from your database
            };

            return View("~/Views/Home/AbrirChamado.cshtml", viewModel);
        }
    }
}