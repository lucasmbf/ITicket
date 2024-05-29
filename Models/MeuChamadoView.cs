namespace ITicket.Models;
public class MeuChamadoView
{
    public int IdChamado { get; set; }
    public string? Solicitante { get; set; }
    public string? Titulo { get; set; }
    public string? DescricaoServico { get; set; }
    public string? Descricao { get; set; }
    public string? Status { get; set; }
    public string? Prioridade { get; set; }
    public DateTime? Abertura { get; set; }
    public string? Fechamento { get; set; }      
    public DateTime? HoraLimite { get; set; } 
    public string? Observacao { get; set; }
    public string? Solucao { get; set; }
    public string? Anexo { get; set; }
    public string? Atendente { get; set; }    

    
}