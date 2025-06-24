using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SigesaData.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipamiento",
                columns: table => new
                {
                    IdEquipamiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamiento", x => x.IdEquipamiento);
                });

            migrationBuilder.CreateTable(
                name: "EstadoReserva",
                columns: table => new
                {
                    IdEstado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoReserva", x => x.IdEstado);
                });

            migrationBuilder.CreateTable(
                name: "RolUsuario",
                columns: table => new
                {
                    IdRolUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolUsuario", x => x.IdRolUsuario);
                });

            migrationBuilder.CreateTable(
                name: "TipoEspacio",
                columns: table => new
                {
                    IdTipoEspacio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEspacio", x => x.IdTipoEspacio);
                });

            migrationBuilder.CreateTable(
                name: "TipoNotificacion",
                columns: table => new
                {
                    IdTipoNotificacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoNotificacion", x => x.IdTipoNotificacion);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroDocumentoIdentidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Espacio",
                columns: table => new
                {
                    IdEspacio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacidad = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdTipoEspacio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Espacio", x => x.IdEspacio);
                    table.ForeignKey(
                        name: "FK_Espacio_TipoEspacio_IdTipoEspacio",
                        column: x => x.IdTipoEspacio,
                        principalTable: "TipoEspacio",
                        principalColumn: "IdTipoEspacio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bitacora",
                columns: table => new
                {
                    IdBitacora = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAccion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bitacora", x => x.IdBitacora);
                    table.ForeignKey(
                        name: "FK_Bitacora_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificacion",
                columns: table => new
                {
                    IdNotificacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoNotificacion = table.Column<int>(type: "int", nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacion", x => x.IdNotificacion);
                    table.ForeignKey(
                        name: "FK_Notificacion_TipoNotificacion_IdTipoNotificacion",
                        column: x => x.IdTipoNotificacion,
                        principalTable: "TipoNotificacion",
                        principalColumn: "IdTipoNotificacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notificacion_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdRolUsuario = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.IdRol);
                    table.ForeignKey(
                        name: "FK_Rol_RolUsuario_IdRolUsuario",
                        column: x => x.IdRolUsuario,
                        principalTable: "RolUsuario",
                        principalColumn: "IdRolUsuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rol_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EspacioEquipamiento",
                columns: table => new
                {
                    IdEspacioEquipamiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEspacio = table.Column<int>(type: "int", nullable: false),
                    IdEquipamiento = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspacioEquipamiento", x => x.IdEspacioEquipamiento);
                    table.ForeignKey(
                        name: "FK_EspacioEquipamiento_Equipamiento_IdEquipamiento",
                        column: x => x.IdEquipamiento,
                        principalTable: "Equipamiento",
                        principalColumn: "IdEquipamiento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EspacioEquipamiento_Espacio_IdEspacio",
                        column: x => x.IdEspacio,
                        principalTable: "Espacio",
                        principalColumn: "IdEspacio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    IdReserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEspacio = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdEstado = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.IdReserva);
                    table.ForeignKey(
                        name: "FK_Reserva_Espacio_IdEspacio",
                        column: x => x.IdEspacio,
                        principalTable: "Espacio",
                        principalColumn: "IdEspacio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_EstadoReserva_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "EstadoReserva",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RolUsuario",
                columns: new[] { "IdRolUsuario", "FechaCreacion", "Nombre" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 17, 5, 17, 5, 364, DateTimeKind.Local).AddTicks(6688), "Administrador" },
                    { 2, new DateTime(2025, 6, 17, 5, 17, 5, 364, DateTimeKind.Local).AddTicks(6707), "Coordinador" },
                    { 3, new DateTime(2025, 6, 17, 5, 17, 5, 364, DateTimeKind.Local).AddTicks(6709), "Usuario" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bitacora_IdUsuario",
                table: "Bitacora",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Espacio_IdTipoEspacio",
                table: "Espacio",
                column: "IdTipoEspacio");

            migrationBuilder.CreateIndex(
                name: "IX_EspacioEquipamiento_IdEquipamiento",
                table: "EspacioEquipamiento",
                column: "IdEquipamiento");

            migrationBuilder.CreateIndex(
                name: "IX_EspacioEquipamiento_IdEspacio",
                table: "EspacioEquipamiento",
                column: "IdEspacio");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacion_IdTipoNotificacion",
                table: "Notificacion",
                column: "IdTipoNotificacion");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacion_IdUsuario",
                table: "Notificacion",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_IdEspacio",
                table: "Reserva",
                column: "IdEspacio");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_IdEstado",
                table: "Reserva",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_IdUsuario",
                table: "Reserva",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Rol_IdRolUsuario",
                table: "Rol",
                column: "IdRolUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Rol_IdUsuario_IdRolUsuario",
                table: "Rol",
                columns: new[] { "IdUsuario", "IdRolUsuario" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bitacora");

            migrationBuilder.DropTable(
                name: "EspacioEquipamiento");

            migrationBuilder.DropTable(
                name: "Notificacion");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Equipamiento");

            migrationBuilder.DropTable(
                name: "TipoNotificacion");

            migrationBuilder.DropTable(
                name: "Espacio");

            migrationBuilder.DropTable(
                name: "EstadoReserva");

            migrationBuilder.DropTable(
                name: "RolUsuario");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "TipoEspacio");
        }
    }
}
