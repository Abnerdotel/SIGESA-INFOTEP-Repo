using Microsoft.EntityFrameworkCore;
using SigesaEntidades;


namespace SigesaData.Context
{
    using Microsoft.EntityFrameworkCore;
    using SigesaEntidades;

    namespace SigesaData.Context
    {
        public partial class SigesaDbContext : DbContext
        {
            public SigesaDbContext() { }

            public SigesaDbContext(DbContextOptions<SigesaDbContext> options) : base(options) { }


            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=DBSIGESA;Integrated Security=True;TrustServerCertificate=True");
                }
            }

            public DbSet<Usuario> Usuarios { get; set; }
            public DbSet<RolUsuario> RolUsuarios { get; set; }
            public DbSet<Rol> Roles { get; set; }
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

                var fechaBase = new DateTime(2024, 1, 1); 
                // Tablas
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

                // Relaciones
                modelBuilder.Entity<Rol>()
                    .HasOne(r => r.Usuario)
                    .WithMany(u => u.Roles)
                    .HasForeignKey(r => r.IdUsuario);

                modelBuilder.Entity<Rol>()
                    .HasOne(r => r.RolUsuario)
                    .WithMany(ru => ru.Roles)
                    .HasForeignKey(r => r.IdRolUsuario);

                modelBuilder.Entity<Reserva>()
                    .HasOne(r => r.Usuario)
                    .WithMany(u => u.Reservas)
                    .HasForeignKey(r => r.IdUsuario);

                modelBuilder.Entity<Reserva>()
                    .HasOne(r => r.Espacio)
                    .WithMany(e => e.Reservas)
                    .HasForeignKey(r => r.IdEspacio);

                modelBuilder.Entity<Reserva>()
                    .HasOne(r => r.Estado)
                    .WithMany(er => er.Reservas)
                    .HasForeignKey(r => r.IdEstado);

                modelBuilder.Entity<Espacio>()
                    .HasOne(e => e.Tipo)
                    .WithMany(t => t.Espacios)
                    .HasForeignKey(e => e.IdTipoEspacio);

                modelBuilder.Entity<Notificacion>()
                    .HasOne(n => n.Usuario)
                    .WithMany(u => u.Notificaciones)
                    .HasForeignKey(n => n.IdUsuario);

                modelBuilder.Entity<Notificacion>()
                    .HasOne(n => n.Tipo)
                    .WithMany(t => t.Notificaciones)
                    .HasForeignKey(n => n.IdTipoNotificacion);

                modelBuilder.Entity<Bitacora>()
                    .HasOne(b => b.Usuario)
                    .WithMany(u => u.Bitacoras)
                    .HasForeignKey(b => b.IdUsuario);

                modelBuilder.Entity<EspacioEquipamiento>()
                    .HasOne(ee => ee.Equipamiento)
                    .WithMany(eq => eq.EspacioEquipamientos)
                    .HasForeignKey(ee => ee.IdEquipamiento);

                modelBuilder.Entity<EspacioEquipamiento>()
                    .HasOne(ee => ee.Espacio)
                    .WithMany(e => e.EspacioEquipamientos)
                    .HasForeignKey(ee => ee.IdEspacio);


                // Restriccion para evitar duplicacion de rol en usuario
                modelBuilder.Entity<Rol>()
                    .HasIndex(r => new { r.IdUsuario, r.IdRolUsuario })
                    .IsUnique();


                // Seeding de valores obligatorios

                //Usuarios
                modelBuilder.Entity<RolUsuario>().HasData(
                    new RolUsuario { IdRolUsuario = 1, Nombre = "Administrador", FechaCreacion = fechaBase },
                    new RolUsuario { IdRolUsuario = 2, Nombre = "Coordinador", FechaCreacion = fechaBase },
                    new RolUsuario { IdRolUsuario = 3, Nombre = "Usuario", FechaCreacion = fechaBase }
                );

                modelBuilder.Entity<EstadoReserva>().HasData(
                    new EstadoReserva { IdEstado = 1, Nombre = "Pendiente", FechaCreacion = fechaBase },
                    new EstadoReserva { IdEstado = 2, Nombre = "Aprobada", FechaCreacion = fechaBase },
                    new EstadoReserva { IdEstado = 3, Nombre = "Cancelada", FechaCreacion = fechaBase }
                );

                modelBuilder.Entity<TipoEspacio>().HasData(
                    new TipoEspacio { IdTipoEspacio = 1, Nombre = "Aula", FechaCreacion = fechaBase },
                    new TipoEspacio { IdTipoEspacio = 2, Nombre = "Sala de Reunión", FechaCreacion = fechaBase },
                    new TipoEspacio { IdTipoEspacio = 3, Nombre = "Laboratorio", FechaCreacion = fechaBase }
                );

                modelBuilder.Entity<TipoNotificacion>().HasData(
                    new TipoNotificacion { IdTipoNotificacion = 1, Nombre = "Confirmación de reserva", FechaCreacion = fechaBase },
                    new TipoNotificacion { IdTipoNotificacion = 2, Nombre = "Rechazo de reserva", FechaCreacion = fechaBase },
                    new TipoNotificacion { IdTipoNotificacion = 3, Nombre = "Recordatorio", FechaCreacion = fechaBase }
                );


            }
        }
    }

}