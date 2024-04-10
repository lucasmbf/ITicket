using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ITicket.Models {
    public class Servico {
        [Key]
        public int IdServico { get; set; }
        public string Prioridade { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }
}
