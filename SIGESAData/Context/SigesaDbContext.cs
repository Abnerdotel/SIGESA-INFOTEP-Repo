using Microsoft.EntityFrameworkCore;
using SigesaEntidades;


namespace SigesaData.Context
{
    public partial class SigesaDbContext : DbContext
    {
        public SigesaDbContext() { }
        public SigesaDbContext(DbContextOptions options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseInMemoryDatabase("DBSIGESA");
        //    }
        //    base.OnConfiguring(optionsBuilder);
        //}

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
            // Mapear tablas
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<RolUsuario>().ToTable("RolUsuario");
            modelBuilder.Entity<Rol>().ToTable("Rol");
            modelBuilder.Entity<Reserva>().ToTable("Reserva");
            modelBuilder.Entity<EstadoReserva>().ToTable("EstadoReserva");
            modelBuilder.Entity<Espacio>().ToTable("Espacio");
            modelBuilder.Entity<TipoEspacio>().ToTable("TipoEspacio");
            modelBuilder.Entity<Equipamiento>().ToTable("Equipamiento");
            modelBuilder.Entity<EspacioEquipamiento>().ToTable("EspacioEquipamiento");
            modelBuilder.Entity<Notificacion>().ToTable("Notificacion");
            modelBuilder.Entity<TipoNotificacion>().ToTable("TipoNotificacion");
            modelBuilder.Entity<Bitacora>().ToTable("Bitacora");

            // Relacion Usuario - Rol (uno a muchos)
            modelBuilder.Entity<Rol>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.Roles)
                .HasForeignKey(r => r.IdUsuario);

            // Relacion Rol - RolUsuario (uno a muchos)
            modelBuilder.Entity<Rol>()
                .HasOne(r => r.RolUsuario)
                .WithMany(ru => ru.Roles)
                .HasForeignKey(r => r.IdRolUsuario);

            // Relacion Usuario - Reserva (uno a muchos)
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.Reservas)
                .HasForeignKey(r => r.IdUsuario);

            // Relacion Espacio - Reserva (uno a muchos)
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Espacio)
                .WithMany(e => e.Reservas)
                .HasForeignKey(r => r.IdEspacio);

            // Relacion EstadoReserva - Reserva (uno a muchos)
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Estado)
                .WithMany(er => er.Reservas)
                .HasForeignKey(r => r.IdEstado);

            // Relacion TipoEspacio - Espacio (uno a muchos)
            modelBuilder.Entity<Espacio>()
                .HasOne(e => e.Tipo)
                .WithMany(t => t.Espacios)
                .HasForeignKey(e => e.IdTipoEspacio);

            // Relacion Usuario - Notificación (uno a muchos)
            modelBuilder.Entity<Notificacion>()
                .HasOne(n => n.Usuario)
                .WithMany(u => u.Notificaciones)
                .HasForeignKey(n => n.IdUsuario);

            // Relacion TipoNotificacion - Notificación (uno a muchos)
            modelBuilder.Entity<Notificacion>()
                .HasOne(n => n.Tipo)
                .WithMany(t => t.Notificaciones)
                .HasForeignKey(n => n.IdTipoNotificacion);

            // Relacion Usuario - Bitácora (uno a muchos)
            modelBuilder.Entity<Bitacora>()
                .HasOne(b => b.Usuario)
                .WithMany(u => u.Bitacoras)
                .HasForeignKey(b => b.IdUsuario);

            // Relacion EspacioEquipamiento - Equipamiento (muchos a uno)
            modelBuilder.Entity<EspacioEquipamiento>()
                .HasOne(ee => ee.Equipamiento)
                .WithMany(eq => eq.EspacioEquipamientos)
                .HasForeignKey(ee => ee.IdEquipamiento);

            // Relacion EspacioEquipamiento - Espacio (muchos a uno)
            modelBuilder.Entity<EspacioEquipamiento>()
                .HasOne(ee => ee.Espacio)
                .WithMany(e => e.EspacioEquipamientos)
                .HasForeignKey(ee => ee.IdEspacio);
        }


    }
}