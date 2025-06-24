using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SigesaData.Context;
using SigesaData.Context.SigesaData.Context;
using SigesaData.Contrato;
using SigesaData.Implementacion;
using SigesaData.Implementacion.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// === Dependencias ===
builder.Services.AddDbContext<SigesaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));

builder.Services.AddScoped<IRolUsuarioRepositorio, RolUsuarioRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IBitacoraRepositorio, BitacoraRepositorio>();
builder.Services.AddScoped<IEquipamientoRepositorio, EquipamientoRepositorio>();
builder.Services.AddScoped<IEspacioRepositorio, EspacioRepositorio>();
builder.Services.AddScoped<INotificacionRepositorio, NotificacionRepositorio>();
builder.Services.AddScoped<IReservaRepositorio, ReservaRepositorio>();
builder.Services.AddScoped<IRolRepositorio, RolRepositorio>();

// === Autenticación con cookies ===
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Acceso/Login";
        options.AccessDeniedPath = "/Acceso/Denegado";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
    });

var app = builder.Build();

// Aplicar migraciones al iniciar
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SigesaDbContext>();
    context.Database.Migrate();
}

// === Middleware ===
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // ← ¡Esto es crucial antes de Authorization!
app.UseAuthorization();

// Ruta por defecto: va al Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.Run();
