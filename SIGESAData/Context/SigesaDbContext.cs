using Microsoft.EntityFrameworkCore;
using SigesaEntidades;


namespace SigesaData.Context
{
    public partial class SigesaDbContext : DbContext
    {
        public SigesaDbContext() { }
        public SigesaDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("DBSIGESA");

            }
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<RolUsuario> RolUsuario { get; set; }
        public DbSet<Rol> Rol {  get; set; }
        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<EstadoReserva> EstadoReserva { get; set; }
        public DbSet<Espacio> Espacio { get; set; }
        public DbSet<TipoEspacio> TipoEspacio { get; set; }
        public DbSet<Equipamiento> Equipamiento { get; set; }
        public DbSet<EspacioEquipamiento> EspacioEquipamiento { get; set; }
        public DbSet<Notificacion> Notificacion { get; set; }
        public DbSet<TipoNotificacion> TipoNotificacion { get; set; }
        public DbSet<Bitacora> Bitacora { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<RolUsuario>().ToTable("RolUsuario");
            modelBuilder.Entity<Rol>().ToTable("Rol");
            modelBuilder.Entity<Reserva>().ToTable("EstadoReserva");
            modelBuilder.Entity<EstadoReserva>().ToTable("EstadoReserva");
            modelBuilder.Entity<Espacio>().ToTable("Espacio");
            modelBuilder.Entity<TipoEspacio>().ToTable("TipoEspacio");
            modelBuilder.Entity<Notificacion>().ToTable("Notificacion");
            modelBuilder.Entity<TipoNotificacion>().ToTable("TipoNotificacion");
            modelBuilder.Entity<Bitacora>().ToTable("Bitacora");

        }
    }
}