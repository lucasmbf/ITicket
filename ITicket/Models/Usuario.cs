using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ITicket.Models {
    public class Usuario {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public int Ramal { get; set; }
        public string Gestor { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public bool IsSolicitante { get; set; }
        public bool IsAtendente { get; set; }
        public bool IsAdm { get; set; }

        public ICollection<Autenticacao> Autenticacao { get; set; }
    }
}
