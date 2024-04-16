using Microsoft.EntityFrameworkCore;

namespace ITicket.Models {
    public class ContextoDb(DbContextOptions<ContextoDb> options) : DbContext(options) {
        public DbSet<Usuario> Usuario { get; set; }
        
        public DbSet<Chamado> Chamado { get; set; }
        
        public DbSet<Servico> Servico { get; set; }

                

    }
}

