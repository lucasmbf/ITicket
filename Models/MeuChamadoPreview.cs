namespace ITicket.Models;
public class MeuChamadoPreview
{
    public int IdChamado { get; set; } 

    public string Solicitante { get; set; } 

    public string Titulo { get; set; }

    public string Descricao { get; set; }    
    
    public DateTime? Abertura { get; set; }       
    
    public DateTime? HoraLimite { get; set; } 

    public string Status { get; set; }   

    
}