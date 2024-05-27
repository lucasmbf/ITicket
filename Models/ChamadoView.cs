namespace ITicket.Models;
public class ChamadoView
{
    public int IdChamado { get; set; }
    public DateTime? Abertura { get; set; }
    public string Solicitante { get; set; }
    public string Status { get; set; }
    public DateTime? HoraLimite { get; set; } 
}