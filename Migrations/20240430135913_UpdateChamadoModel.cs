using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITicket.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChamadoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Usuario",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Usuario",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "IdUsuario");

            migrationBuilder.CreateTable(
                name: "Servico",
                columns: table => new
                {
                    IdServico = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Prioridade = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servico", x => x.IdServico);
                });

            migrationBuilder.CreateTable(
                name: "Chamado",
                columns: table => new
                {
                    IdChamado = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Prioridade = table.Column<string>(type: "TEXT", nullable: false),
                    Abertura = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Fechamento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    HoraLimite = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Observacao = table.Column<string>(type: "TEXT", nullable: false),
                    Solucao = table.Column<string>(type: "TEXT", nullable: false),
                    Anexo = table.Column<string>(type: "TEXT", nullable: false),
                    IdUsuarioSolicitante = table.Column<int>(type: "INTEGER", nullable: false),
                    IdServico = table.Column<int>(type: "INTEGER", nullable: false),
                    Massivo = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chamado", x => x.IdChamado);
                    table.ForeignKey(
                        name: "FK_Chamado_Servico_IdServico",
                        column: x => x.IdServico,
                        principalTable: "Servico",
                        principalColumn: "IdServico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chamado_Usuario_IdUsuarioSolicitante",
                        column: x => x.IdUsuarioSolicitante,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chamado_IdServico",
                table: "Chamado",
                column: "IdServico");

            migrationBuilder.CreateIndex(
                name: "IX_Chamado_IdUsuarioSolicitante",
                table: "Chamado",
                column: "IdUsuarioSolicitante");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chamado");

            migrationBuilder.DropTable(
                name: "Servico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Usuario");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "IdUsuario");
        }
    }
}
