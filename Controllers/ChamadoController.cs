using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ITicket.Models;
using ITicket.Controllers;

public class ChamadoController : Controller
{
    private readonly ILogger<ChamadoController> _logger;
    private readonly ContextoDb _contexto; 

    public ChamadoController(ILogger<ChamadoController> logger, ContextoDb contexto)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
        ViewBag.Servico = servicos;

        var username = HttpContext.Session.GetString("Username"); 
        var usuario = _contexto.Usuario.FirstOrDefault(u => u.Username == username); 

        var viewModel = new AbrirChamadoViewModel
        {
            Chamados = chamados,
            Usuario = usuario
        };       
        

        return View("~/Views/Home/AbrirChamado.cshtml", viewModel);
    }

    [HttpPost]
    public IActionResult AbrirChamado(Chamado chamado)
    {
        if (ModelState.IsValid)
        {
            try
            {
                
                
                chamado.Massivo = Request.Form["Massivo"].ToString().ToLower() == "sim";              

                


                _contexto.Chamado.Add(chamado);
                _contexto.SaveChanges();
                TempData["SuccessMessage"] = "Chamado aberto com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception using the built-in ILogger interface
                _logger.LogError(ex, "An error occurred while opening the chamado");

                // Create a new ViewModel here
                var errorViewModel = new AbrirChamadoViewModel
                {
                    Chamados = new List<Chamado>(), // Empty list
                    // Populate Usuario property as necessary
                };

                // Add a model error to display the error message in the view
                ModelState.AddModelError(string.Empty, "An error occurred while opening the chamado.");

                return View("~/Views/Home/AbrirChamado.cshtml", errorViewModel);
            }
        }

        var viewModel = new AbrirChamadoViewModel
        {
            Chamados = new List<Chamado> { chamado },
            // Populate Usuario property as necessary
        };

        return View("~/Views/Home/AbrirChamado.cshtml", viewModel);
    }
}