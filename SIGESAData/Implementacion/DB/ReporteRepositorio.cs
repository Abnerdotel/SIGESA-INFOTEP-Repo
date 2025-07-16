using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using SigesaData.Context;
using SigesaData.Context.SigesaData.Context;
using SigesaData.Contrato;
using System.IO;

namespace SigesaData.Implementacion.DB
{
    public class ReporteRepositorio : IReporteRepositorio
    {
        private readonly SigesaDbContext _context;

        public ReporteRepositorio(SigesaDbContext context)
        {
            _context = context;
        }

        public async Task<byte[]> GenerarReporteReservasAsync(DateTime inicio, DateTime fin)
        {
            var reservas = await _context.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Espacio)
                .Include(r => r.Estado)
                .Where(r => r.FechaInicio >= inicio && r.FechaFin <= fin)
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Reservas");

            ws.Cell(1, 1).Value = "Usuario";
            ws.Cell(1, 2).Value = "Espacio";
            ws.Cell(1, 3).Value = "Estado";
            ws.Cell(1, 4).Value = "Fecha Inicio";
            ws.Cell(1, 5).Value = "Fecha Fin";

            int row = 2;
            foreach (var r in reservas)
            {
                ws.Cell(row, 1).Value = $"{r.Usuario.Nombre} {r.Usuario.Apellido}";
                ws.Cell(row, 2).Value = r.Espacio.Nombre;
                ws.Cell(row, 3).Value = r.Estado.Nombre;
                ws.Cell(row, 4).Value = r.FechaInicio.ToString("g");
                ws.Cell(row, 5).Value = r.FechaFin.ToString("g");
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public async Task<byte[]> GenerarReporteUsuariosAsync()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.Roles)
                .ThenInclude(r => r.RolUsuario)
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Usuarios");

            ws.Cell(1, 1).Value = "Nombre";
            ws.Cell(1, 2).Value = "Apellido";
            ws.Cell(1, 3).Value = "Correo";
            ws.Cell(1, 4).Value = "Rol";
            ws.Cell(1, 5).Value = "Activo";

            int row = 2;
            foreach (var u in usuarios)
            {
                ws.Cell(row, 1).Value = u.Nombre;
                ws.Cell(row, 2).Value = u.Apellido;
                ws.Cell(row, 3).Value = u.Correo;
                ws.Cell(row, 4).Value = u.Roles.FirstOrDefault()?.RolUsuario.Nombre ?? "Sin rol";
                ws.Cell(row, 5).Value = u.EstaActivo ? "Sí" : "No";
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public async Task<byte[]> GenerarReporteEspaciosAsync()
        {
            var espacios = await _context.Espacios
                .Include(e => e.Tipo)
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Espacios");

            ws.Cell(1, 1).Value = "Nombre";
            ws.Cell(1, 2).Value = "Tipo";
            ws.Cell(1, 3).Value = "Capacidad";
            ws.Cell(1, 4).Value = "Observaciones";

            int row = 2;
            foreach (var e in espacios)
            {
                ws.Cell(row, 1).Value = e.Nombre;
                ws.Cell(row, 2).Value = e.Tipo.Nombre;
                ws.Cell(row, 3).Value = e.Capacidad;
                ws.Cell(row, 4).Value = e.Observaciones ?? "N/A";
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
