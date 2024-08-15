using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMinhasFinancas.Migrations
{
    public partial class AdicionandocampoNomeCompletonousuário : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "ComprovantesDB",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NomeCompleto",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ComprovantesDB_UsuarioId",
                table: "ComprovantesDB",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComprovantesDB_AspNetUsers_UsuarioId",
                table: "ComprovantesDB",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComprovantesDB_AspNetUsers_UsuarioId",
                table: "ComprovantesDB");

            migrationBuilder.DropIndex(
                name: "IX_ComprovantesDB_UsuarioId",
                table: "ComprovantesDB");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "ComprovantesDB");

            migrationBuilder.DropColumn(
                name: "NomeCompleto",
                table: "AspNetUsers");
        }
    }
}
