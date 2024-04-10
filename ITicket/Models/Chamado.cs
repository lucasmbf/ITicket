using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ITicket.Models {
    public class Chamado {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdChamado { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Prioridade { get; set; } = string.Empty;
        public string DataAbertura { get; set; } = string.Empty;
        public string HoraAbertura { get; set; } = string.Empty;
        public string DataFechamento { get; set; } = string.Empty;
        public string HoraFechamento { get; set; } = string.Empty;
        public string DataLimite { get; set; } = string.Empty;
        public string HoraLimite { get; set; } = string.Empty;
        public string Observacao { get; set; } = string.Empty;
        public string Solucao { get; set; } = string.Empty;
        public string Anexo { get; set; } = string.Empty;
        public int IdUsuarioSolicitante { get; set; }
        public int IdServico { get; set; }
    }
}
