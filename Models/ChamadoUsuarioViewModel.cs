namespace ITicket.Models
{
    public class ChamadoUsuarioViewModel
    {
        public Chamado Chamado { get; set; } 
        public Usuario Usuario { get; set; }
        public List<string> Servico { get; set; }

        public ChamadoUsuarioViewModel()
    {
        Chamado = new Chamado();
        Usuario = new Usuario();
    }

    }
}