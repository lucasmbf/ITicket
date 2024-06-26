using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ITicket.Models;
using System.ComponentModel;


public class ChamadoController : Controller
{
    private readonly ContextoDb _contexto;
    private readonly IEmailService _emailService;

    public ChamadoController(ContextoDb context, IEmailService emailService)
    {
        _contexto = context;
        _emailService = emailService;
    }


    //metodo para buscar prioridade por descrição / method to search priority by description
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

     //metodo popular campos Nome, Departamento, Cargo e Email / method to populate fields Name, Department, Position and Email 
    [HttpGet]
    public IActionResult AbrirChamado()
    {
        var loggedInUsername = HttpContext.Session.GetString("Username");
        var user = _contexto.Usuario.FirstOrDefault(u => u.Username == loggedInUsername);
        var servicos = _contexto.Servico.Select(s => s.Descricao).ToList();
        var email = user.Email;

        var viewModel = new ChamadoUsuarioViewModel
        {
            Usuario = new Usuario
            {
                Nome = user.Nome,
                Departamento = user.Departamento,
                Cargo = user.Cargo,
                Email = user.Email,
            },
            Servico = servicos // adiciona os valores de Descricao ao view / adds the values of Descricao to the view
        };

        return View("~/Views/Home/AbrirChamado.cshtml", viewModel);
    }

    //metodo para abrir chamado / method to open a ticket
    [HttpPost]
    public async Task<IActionResult> AbrirChamado(Chamado chamado)
    {
        if (ModelState.IsValid)
        {
            chamado.Abertura = DateTime.Now;
            chamado.Status = "Novo";
            if (chamado.Servico != null)
            {
                var servico = _contexto.Servico.FirstOrDefault(s => s.Descricao == chamado.Servico.Descricao);

                if (servico != null)
                {
                    chamado.Servico = servico;
                    chamado.DescricaoServico = servico.Descricao;

                    if (servico.IdServico == null) // verifica se servico é null / checks if servico is null
                    {
                        ModelState.AddModelError("", "Service Id is null.");
                        return View("~/Views/Home/Error.cshtml");
                    }

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

                    // calculo SLA / SLA calculation
                    switch (chamado.Prioridade)
                    {
                        case "Alta":
                            chamado.HoraLimite = chamado.Abertura.GetValueOrDefault().AddHours(isMassivo ? 1 : 1);
                            break;
                        case "Media":
                            chamado.HoraLimite = chamado.Abertura.GetValueOrDefault().AddHours(isMassivo ? 2 : 24);
                            break;
                        case "Baixa":
                        default:
                            if (!isMassivo)
                            {
                                chamado.HoraLimite = chamado.Abertura.GetValueOrDefault().AddDays(5);
                            }
                            break;
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

            var username = HttpContext.Session.GetString("Username");
            var usuario = _contexto.Usuario.FirstOrDefault(u => u.Username == username);

            if (usuario != null)
            {
                await _emailService.SendEmailAsync(usuario.Email, "iticket.lab@gmail.com", chamado.Titulo, chamado.Descricao);
            }
            else
            {
                ModelState.AddModelError("", "The user does not exist.");
                return View("~/Views/Home/Error.cshtml");
            }

            TempData["Message"] = "Chamado Aberto com Sucesso!";

            var viewModel = new ChamadoUsuarioViewModel
            {
                Chamado = chamado,
                Usuario = usuario,
                Servico = _contexto.Servico.Select(s => s.Descricao).ToList() // busca todas as descricoes de serviço do banco
            };

            return View("~/Views/Home/AbrirChamado.cshtml", viewModel);
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Nao foi possivel abrir o chamado.");

            var username = HttpContext.Session.GetString("Username");
            var usuario = _contexto.Usuario.FirstOrDefault(u => u.Username == username);

            var viewModel = new ChamadoUsuarioViewModel
            {
                Chamado = chamado,
                Usuario = usuario,
                Servico = _contexto.Servico.Select(s => s.Descricao).ToList() // busca todas as descricoes de serviço do banco
            };

            return View("~/Views/Home/AbrirChamado.cshtml", viewModel);
        }
    }
}