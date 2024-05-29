using ITicket.Controllers;
using Microsoft.EntityFrameworkCore;

namespace ITicket.Models {
    public class ContextoDb(DbContextOptions<ContextoDb> options) : DbContext(options) {
        public DbSet<Usuario> Usuario { get; set; }
        
        public DbSet<Chamado> Chamado { get; set; }
        
        public DbSet<Servico> Servico { get; set; }

        public DbSet<ChamadoView> ChamadoView { get; set; }   

        public DbSet<MeuChamadoView> MeuChamadoView { get; set; }    

        public DbSet<MeuChamadoPreview> MeuChamadoPreview { get; set; }     
           


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChamadoView>(entity =>
            {
                entity.HasNoKey(); // Because views don't have primary keys

                entity.ToView("ChamadoView"); // Map to the view in the database
            });

            modelBuilder.Entity<MeuChamadoView>(entity =>
            {
                entity.HasNoKey(); 
                entity.ToView("MeuChamadoView"); 
            });

            modelBuilder.Entity<MeuChamadoPreview>(entity =>
            {
                entity.HasNoKey(); 
                entity.ToView("MeuChamadoPreview"); 
            });

            
        }
    }
}

