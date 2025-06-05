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

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RolUsuario> RolUsuarios { get; set; }
        public DbSet<Rol> Roles {  get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<EstadoReserva> EstadoReservas { get; set; }
        public DbSet<Espacio> Espacios { get; set; }
        public DbSet<TipoEspacio> TipoEspacios { get; set; }
        public DbSet<Equipamiento> Equipamientos { get; set; }
        public DbSet<EspacioEquipamiento> EspacioEquipamientos { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<TipoNotificacion> TipoNotificaciones { get; set; }
        public DbSet<Bitacora> Bitacoras { get; set; }



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