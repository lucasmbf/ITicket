using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ITicket.Models; 

public class ChamadoController : Controller
{
    private readonly ContextoDb _context; 

    public ChamadoController(ContextoDb context) 
    {
        _context = context; 
    }

    [HttpPost]
    public IActionResult Create(ITicket.Models.Chamado chamado)
    {
        if (ModelState.IsValid)
        {
            chamado.HoraAbertura = DateTime.Now; 
            chamado.DataAbertura = DateTime.Now; 

            // Classify priority
            var servico = _context.Servico.Find(chamado.IdServico);
            if (servico.Descricao == "Alta" || chamado.AfetaMaisUsuarios) 
            {
                chamado.Prioridade = "Alta";
            }
            else if (servico.Descricao == "Media")
            {
                chamado.Prioridade = "Media";
            }
            else
            {
                chamado.Prioridade = "Baixa";
            }

            _context.Chamado.Add(chamado);
            _context.SaveChanges();

            return Json(new { success = true });
        }

        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        return Json(new { success = false, errors = errors });
    }
}