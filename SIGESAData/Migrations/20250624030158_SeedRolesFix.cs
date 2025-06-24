using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SigesaData.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EstadoReserva",
                columns: new[] { "IdEstado", "FechaCreacion", "Nombre" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pendiente" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aprobada" },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cancelada" }
                });

            migrationBuilder.UpdateData(
                table: "RolUsuario",
                keyColumn: "IdRolUsuario",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "RolUsuario",
                keyColumn: "IdRolUsuario",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "RolUsuario",
                keyColumn: "IdRolUsuario",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "TipoEspacio",
                columns: new[] { "IdTipoEspacio", "FechaCreacion", "Nombre" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aula" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sala de Reunión" },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Laboratorio" }
                });

            migrationBuilder.InsertData(
                table: "TipoNotificacion",
                columns: new[] { "IdTipoNotificacion", "FechaCreacion", "Nombre" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Confirmación de reserva" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rechazo de reserva" },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Recordatorio" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EstadoReserva",
                keyColumn: "IdEstado",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EstadoReserva",
                keyColumn: "IdEstado",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EstadoReserva",
                keyColumn: "IdEstado",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TipoEspacio",
                keyColumn: "IdTipoEspacio",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TipoEspacio",
                keyColumn: "IdTipoEspacio",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TipoEspacio",
                keyColumn: "IdTipoEspacio",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TipoNotificacion",
                keyColumn: "IdTipoNotificacion",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TipoNotificacion",
                keyColumn: "IdTipoNotificacion",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TipoNotificacion",
                keyColumn: "IdTipoNotificacion",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "RolUsuario",
                keyColumn: "IdRolUsuario",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2025, 6, 17, 5, 17, 5, 364, DateTimeKind.Local).AddTicks(6688));

            migrationBuilder.UpdateData(
                table: "RolUsuario",
                keyColumn: "IdRolUsuario",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2025, 6, 17, 5, 17, 5, 364, DateTimeKind.Local).AddTicks(6707));

            migrationBuilder.UpdateData(
                table: "RolUsuario",
                keyColumn: "IdRolUsuario",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2025, 6, 17, 5, 17, 5, 364, DateTimeKind.Local).AddTicks(6709));
        }
    }
}
