// Implementación de IBitacoraRepositorio con Entity Framework Core

using Microsoft.EntityFrameworkCore;
using SigesaData.Context;
using SigesaData.Context.SigesaData.Context;
using SigesaData.Contrato;
using SigesaData.Utilidades;
using SigesaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly SigesaDbContext _context;

    public UsuarioRepositorio(SigesaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> ObtenerListaAsync(int idRolUsuario = 0)
    {
        var query = _context.Usuarios
            .AsNoTracking()
            .Include(u => u.Roles)
                .ThenInclude(r => r.RolUsuario)
            .AsQueryable();

        if (idRolUsuario > 0)
        {
            query = query.Where(u => u.Roles.Any(r => r.IdRolUsuario == idRolUsuario));
        }

        return await query.ToListAsync();
    }

    public async Task<Usuario?> AutenticarAsync(string documentoIdentidad, string clave)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Roles)
                .ThenInclude(r => r.RolUsuario)
            .FirstOrDefaultAsync(u => u.NumeroDocumentoIdentidad == documentoIdentidad && u.EstaActivo);

        if (usuario == null) return null;

        return Encriptador.VerificarClave(clave, usuario.Clave) ? usuario : null;
    }

    public async Task<int> GuardarAsync(Usuario usuario)
    {
        bool existeDocumento = await _context.Usuarios.AnyAsync(u => u.NumeroDocumentoIdentidad == usuario.NumeroDocumentoIdentidad);
        if (existeDocumento)
            throw new InvalidOperationException("Ya existe un usuario con el mismo número de documento.");

        usuario.Clave = Encriptador.HashearClave(usuario.Clave);
        usuario.FechaCreacion = DateTime.Now;
        usuario.EstaActivo = true;

        var rolesParaAsignar = usuario.Roles.ToList();
        usuario.Roles = new List<Rol>();

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        foreach (var rol in rolesParaAsignar)
        {
            rol.IdUsuario = usuario.IdUsuario;
            rol.FechaCreacion = DateTime.Now;
            _context.Roles.Add(rol);
        }

        await _context.SaveChangesAsync();
        return usuario.IdUsuario;
    }

    public async Task<bool> EditarAsync(Usuario usuario)
    {
        var existente = await _context.Usuarios
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.IdUsuario == usuario.IdUsuario);

        if (existente == null) return false;

        existente.NumeroDocumentoIdentidad = usuario.NumeroDocumentoIdentidad;
        existente.Nombre = usuario.Nombre;
        existente.Apellido = usuario.Apellido;
        existente.Correo = usuario.Correo;

        if (!string.IsNullOrWhiteSpace(usuario.Clave))
        {
            existente.Clave = Encriptador.HashearClave(usuario.Clave);
        }

        if (usuario.Roles.Any())
        {
            var nuevoRolId = usuario.Roles.First().IdRolUsuario;
            var rolActual = existente.Roles.FirstOrDefault();

            if (rolActual == null)
            {
                existente.Roles.Add(new Rol
                {
                    IdUsuario = existente.IdUsuario,
                    IdRolUsuario = nuevoRolId,
                    FechaCreacion = DateTime.Now
                });
            }
            else if (rolActual.IdRolUsuario != nuevoRolId)
            {
                rolActual.IdRolUsuario = nuevoRolId;
                rolActual.FechaCreacion = DateTime.Now;
            }
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return false;

        usuario.EstaActivo = false;
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();

        return true;
    }
}
