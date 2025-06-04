
using Microsoft.EntityFrameworkCore;
using SigesaData.Configuracion;
using SigesaData.Context;
using SigesaData.Contrato;
using SigesaData.Implementacion.DB;

namespace SigesaWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var conn = builder.Configuration.GetConnectionString("ConnectionStrings");
            builder.Services.AddDbContext<SigesaDbContext>(x => x.UseSqlServer(conn));


            builder.Services.AddScoped<IRolUsuarioRepositorio, RolUsuarioRepositorio>();
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<IBitacoraRepositorio, BitacoraRepositorio>();
            builder.Services.AddScoped<IEquipamientoRepositorio, EquipamientoRepositorio>();
            builder.Services.AddScoped<IEspacioRepositorio, EspacioRepositorio>();
            builder.Services.AddScoped<INotificacionRepositorio, NotificacionRepositorio>();
            builder.Services.AddScoped<IReservaRepositorio, ReservaRepositorio>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
