using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using ITicket.Models;

public class Chamado
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdChamado { get; set; }

    [Required]
    public string Titulo { get; set; } = string.Empty;

    [Required]
    public string Descricao { get; set; } = string.Empty;

   private string _status = "Novo";
    [Required]
    public string Status 
{ 
    get { return _status; }
    set { _status = value; }
}

    [Required]
    public string Prioridade { get; set; } = string.Empty;

    public DateTime? Abertura { get; set; }

    public DateTime? Fechamento { get; set; }

    public DateTime? HoraLimite { get; set; }

    public string Observacao { get; set; } = string.Empty;

    public string Solucao { get; set; } = string.Empty;

    public string Anexo { get; set; } = string.Empty;

    public string Solicitante { get; set; } = string.Empty;

    public string Atendente { get; set; } = string.Empty;

    public string DescricaoServico { get; set; } = string.Empty;

    public string Massivo { get; set; } = string.Empty;

    public int IdServico { get; set; }

    public Servico Servico { get; set; }
}