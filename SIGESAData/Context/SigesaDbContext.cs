using Microsoft.EntityFrameworkCore;
using SigesaEntidades;
using System;


namespace SigesaData.Context
{
    public partial class SigesaDbContext : DbContext
    {



        //public SigesaDbContext(DbContextOptions<SigesaDbContext> options) : base(options) { }

        public SigesaDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                 optionsBuilder.UseInMemoryDatabase("DBSIGESA");
                //optionsBuilder.UseInMemoryDatabase("sigesadb");
                
            }
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<RolUsuario> RolUsuario { get; set; }
        public DbSet<Rol> Roles {get; set;} 
        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<EstadoReserva> EstadoReserva { get; set; }
        public DbSet<Espacio> Espacio { get; set; }
        public DbSet<TipoEspacio> TipoEspacio { get; set; }
        public DbSet<Equipamiento> Equipamiento { get; set; }
        public DbSet<EspacioEquipamiento> EspacioEquipamiento { get; set; }
        public DbSet<Notificacion> Notificacion { get; set; }
        public DbSet<TipoNotificacion> TipoNotificacion { get; set; }
        public DbSet<Bitacora> Bitacora { get; set; }
    }
}