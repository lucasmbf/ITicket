using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITicket.Models
{
    public class Chamado
{
        public Chamado()
        {
            Titulo = string.Empty;
            Descricao = string.Empty;
            Status = "Novo";
            Prioridade = string.Empty;
            Observacao = string.Empty;
            Solucao = string.Empty;
            Anexo = string.Empty;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdChamado { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string Status { get; set; }

        public string Prioridade { get; set; }

        public DateTime? Abertura { get; set; }

        public DateTime? Fechamento { get; set; }

        public DateTime? HoraLimite { get; set; }

        public string Observacao { get; set; }

        public string Solucao { get; set; }

        public string Anexo { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuarioSolicitante { get; set; }
        public Usuario? Usuario { get; set; } // Navigation property

        [ForeignKey("Servico")]
        public int IdServico { get; set; }
        public Servico? Servico { get; set; } // Navigation property

        public bool? Massivo { get; set; }
    }
}