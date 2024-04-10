using Microsoft.EntityFrameworkCore;

namespace ITicket.Models {
    public class Contexto : DbContext {
        public DbSet<Usuario> Usuarios { get; set; }
        
        public DbSet<Chamado> Chamado { get; set; }
        
        public DbSet<Servico> Servico { get; set; }


        public Contexto(DbContextOptions<Contexto> options) : base(options) {
        }

    }
}

