using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMinhasFinancas.Migrations
{
    public partial class IncluindoCampodeUsuárioemcadatabelaparaidentificarosregistrosdecadausuário : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "TransferenciasDB",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "TipoContasDB",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "MetasDB",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "FormasPgtoDB",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TransferenciasDB_UsuarioId",
                table: "TransferenciasDB",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoContasDB_UsuarioId",
                table: "TipoContasDB",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MetasDB_UsuarioId",
                table: "MetasDB",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_FormasPgtoDB_UsuarioId",
                table: "FormasPgtoDB",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormasPgtoDB_AspNetUsers_UsuarioId",
                table: "FormasPgtoDB",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetasDB_AspNetUsers_UsuarioId",
                table: "MetasDB",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TipoContasDB_AspNetUsers_UsuarioId",
                table: "TipoContasDB",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferenciasDB_AspNetUsers_UsuarioId",
                table: "TransferenciasDB",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormasPgtoDB_AspNetUsers_UsuarioId",
                table: "FormasPgtoDB");

            migrationBuilder.DropForeignKey(
                name: "FK_MetasDB_AspNetUsers_UsuarioId",
                table: "MetasDB");

            migrationBuilder.DropForeignKey(
                name: "FK_TipoContasDB_AspNetUsers_UsuarioId",
                table: "TipoContasDB");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferenciasDB_AspNetUsers_UsuarioId",
                table: "TransferenciasDB");

            migrationBuilder.DropIndex(
                name: "IX_TransferenciasDB_UsuarioId",
                table: "TransferenciasDB");

            migrationBuilder.DropIndex(
                name: "IX_TipoContasDB_UsuarioId",
                table: "TipoContasDB");

            migrationBuilder.DropIndex(
                name: "IX_MetasDB_UsuarioId",
                table: "MetasDB");

            migrationBuilder.DropIndex(
                name: "IX_FormasPgtoDB_UsuarioId",
                table: "FormasPgtoDB");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "TransferenciasDB");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "TipoContasDB");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "MetasDB");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "FormasPgtoDB");
        }
    }
}
