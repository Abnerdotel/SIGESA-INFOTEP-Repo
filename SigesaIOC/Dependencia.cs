
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SigesaData.Contrato;
using SigesaData.Implementacion.DB;
using SigesaData.Context;

namespace SigesaIOC
{
    public static class Depedencia
    {
        public static void InyectarDependencia(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<SigesaDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CadenaSQL"));

            });

            services.AddScoped<IRolUsuarioRepositorio, RolUsuarioRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IBitacoraRepositorio, BitacoraRepositorio>();
            services.AddScoped<IEquipamientoRepositorio, EquipamientoRepositorio>();
            services.AddScoped<IEspacioRepositorio, EspacioRepositorio>();
            services.AddScoped<INotificacionRepositorio, NotificacionRepositorio>();
            services.AddScoped<IReservaRepositorio, ReservaRepositorio>();


        }

    }
}
