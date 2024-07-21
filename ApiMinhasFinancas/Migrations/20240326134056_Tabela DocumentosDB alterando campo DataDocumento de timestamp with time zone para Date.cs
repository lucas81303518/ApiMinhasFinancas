using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMinhasFinancas.Migrations
{
    public partial class TabelaDocumentosDBalterandocampoDataDocumentodetimestampwithtimezoneparaDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DocumentosDB_FormaPagamentoId",
                table: "DocumentosDB");

            migrationBuilder.DropIndex(
                name: "IX_DocumentosDB_TipoContaId",
                table: "DocumentosDB");

            migrationBuilder.DropIndex(
                name: "IX_DocumentosDB_UsuarioId",
                table: "DocumentosDB");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosDB_FormaPagamentoId",
                table: "DocumentosDB",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosDB_TipoContaId",
                table: "DocumentosDB",
                column: "TipoContaId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosDB_UsuarioId",
                table: "DocumentosDB",
                column: "UsuarioId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataDocumento",
                table: "DocumentosDB",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DocumentosDB_FormaPagamentoId",
                table: "DocumentosDB");

            migrationBuilder.DropIndex(
                name: "IX_DocumentosDB_TipoContaId",
                table: "DocumentosDB");

            migrationBuilder.DropIndex(
                name: "IX_DocumentosDB_UsuarioId",
                table: "DocumentosDB");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosDB_FormaPagamentoId",
                table: "DocumentosDB",
                column: "FormaPagamentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosDB_TipoContaId",
                table: "DocumentosDB",
                column: "TipoContaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosDB_UsuarioId",
                table: "DocumentosDB",
                column: "UsuarioId",
                unique: true);
        }
    }
}
