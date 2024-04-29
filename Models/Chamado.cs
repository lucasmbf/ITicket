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

        public DateTime? DataAbertura { get; set; }

        public DateTime? HoraAbertura { get; set; }

        public DateTime? DataFechamento { get; set; }

        public DateTime? HoraFechamento { get; set; }

        public DateTime? HoraLimite { get; set; }

        public string Observacao { get; set; }

        public string Solucao { get; set; }

        public string Anexo { get; set; }

        public int IdUsuarioSolicitante { get; set; }

        public int IdServico { get; set; }

        public bool AfetaMaisUsuarios { get; set; }
    }
}