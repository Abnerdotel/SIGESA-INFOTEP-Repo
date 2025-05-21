

using Microsoft.EntityFrameworkCore;
using SigesaEntidades;

namespace SigesaData.Context
{
    public class ReservaContext : DbContext
    {
        public ReservaContext(DbContext options): base () { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("DBSIGESA");

            }
            base.OnConfiguring(optionsBuilder);
        }
        
        public DbSet<Reserva> Reservas { get; set; }
    }
}
