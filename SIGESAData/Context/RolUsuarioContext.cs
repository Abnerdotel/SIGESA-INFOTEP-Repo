using Microsoft.EntityFrameworkCore;
using SigesaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigesaData.Context
{
    public class RolUsuarioContext : DbContext
    {
        public RolUsuarioContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("DBSIGESA");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<RolUsuario> RolesUsuario { get; set; }
    }
}
