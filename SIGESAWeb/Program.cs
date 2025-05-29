
using Microsoft.AspNetCore.Authentication.Cookies;
using SigesaData.Configuracion;
using SigesaData.Contrato;
using SigesaData.Implementacion.DB;
using SigesaIOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.InyectarDependencia(builder.Configuration);
//builder.Services.InyectarDependencia(builder.Configuration);

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddScoped<IRolUsuarioRepositorio, RolUsuarioRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IBitacoraRepositorio, BitacoraRepositorio>();
builder.Services.AddScoped<IEquipamientoRepositorio, EquipamientoRepositorio>();
builder.Services.AddScoped<IEspacioRepositorio, EspacioRepositorio>();
builder.Services.AddScoped<INotificacionRepositorio, NotificacionRepositorio>();
builder.Services.AddScoped<IReservaRepositorio, ReservaRepositorio>();

//builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Acceso/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        option.AccessDeniedPath = "/Acceso/Denegado";
    });

//builder.Services.AddEndpointsApiExplorer();//
//builder.Services.AddSwaggerGen();//

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}


//app.UseSwagger();
//app.UseSwaggerUI();


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.Run();
