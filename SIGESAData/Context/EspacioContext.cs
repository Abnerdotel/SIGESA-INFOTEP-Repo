

using Microsoft.EntityFrameworkCore;
using SigesaEntidades;

namespace SigesaData.Context
{
    public class EspacioContext : DbContext
    {
        public EspacioContext(DbContext options ): base () { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("DBSIGESA");

            }
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Espacio> Espacios { get; set; }
    }
}
