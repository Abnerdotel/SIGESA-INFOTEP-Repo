

using Microsoft.EntityFrameworkCore;
using SigesaEntidades;

namespace SigesaData.Context
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseInMemoryDatabase("DBSIGESA");
                
            }
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<RolUsuario> RolUsuario { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
