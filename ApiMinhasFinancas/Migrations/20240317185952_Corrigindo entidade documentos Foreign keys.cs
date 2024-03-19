using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMinhasFinancas.Migrations
{
    public partial class CorrigindoentidadedocumentosForeignkeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentosDB_FormasPgtoDB_FormasPgtoDB",
                table: "DocumentosDB");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentosDB_TipoContasDB_TipoContasDB",
                table: "DocumentosDB");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentosDB_UsuariosDB_UsuariosDB",
                table: "DocumentosDB");

            migrationBuilder.RenameColumn(
                name: "UsuariosDB",
                table: "DocumentosDB",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "TipoContasDB",
                table: "DocumentosDB",
                newName: "TipoContaId");

            migrationBuilder.RenameColumn(
                name: "FormasPgtoDB",
                table: "DocumentosDB",
                newName: "FormaPagamentoId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentosDB_UsuariosDB",
                table: "DocumentosDB",
                newName: "IX_DocumentosDB_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentosDB_TipoContasDB",
                table: "DocumentosDB",
                newName: "IX_DocumentosDB_TipoContaId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentosDB_FormasPgtoDB",
                table: "DocumentosDB",
                newName: "IX_DocumentosDB_FormaPagamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentosDB_FormasPgtoDB_FormaPagamentoId",
                table: "DocumentosDB",
                column: "FormaPagamentoId",
                principalTable: "FormasPgtoDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentosDB_TipoContasDB_TipoContaId",
                table: "DocumentosDB",
                column: "TipoContaId",
                principalTable: "TipoContasDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentosDB_UsuariosDB_UsuarioId",
                table: "DocumentosDB",
                column: "UsuarioId",
                principalTable: "UsuariosDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentosDB_FormasPgtoDB_FormaPagamentoId",
                table: "DocumentosDB");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentosDB_TipoContasDB_TipoContaId",
                table: "DocumentosDB");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentosDB_UsuariosDB_UsuarioId",
                table: "DocumentosDB");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "DocumentosDB",
                newName: "UsuariosDB");

            migrationBuilder.RenameColumn(
                name: "TipoContaId",
                table: "DocumentosDB",
                newName: "TipoContasDB");

            migrationBuilder.RenameColumn(
                name: "FormaPagamentoId",
                table: "DocumentosDB",
                newName: "FormasPgtoDB");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentosDB_UsuarioId",
                table: "DocumentosDB",
                newName: "IX_DocumentosDB_UsuariosDB");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentosDB_TipoContaId",
                table: "DocumentosDB",
                newName: "IX_DocumentosDB_TipoContasDB");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentosDB_FormaPagamentoId",
                table: "DocumentosDB",
                newName: "IX_DocumentosDB_FormasPgtoDB");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentosDB_FormasPgtoDB_FormasPgtoDB",
                table: "DocumentosDB",
                column: "FormasPgtoDB",
                principalTable: "FormasPgtoDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentosDB_TipoContasDB_TipoContasDB",
                table: "DocumentosDB",
                column: "TipoContasDB",
                principalTable: "TipoContasDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentosDB_UsuariosDB_UsuariosDB",
                table: "DocumentosDB",
                column: "UsuariosDB",
                principalTable: "UsuariosDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
