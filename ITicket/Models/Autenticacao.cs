using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ITicket.Models {
    public class Autenticacao {


        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public Usuario Usuario { get; set; }
    }

}
