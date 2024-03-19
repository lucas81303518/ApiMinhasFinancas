using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiMinhasFinancas.Migrations
{
    public partial class AlteraçoesEntidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComprovantesDB_DocumentosDB_Id",
                table: "ComprovantesDB");

            migrationBuilder.DropIndex(
                name: "IX_DocumentosDB_FormaPagamentoId",
                table: "DocumentosDB");

            migrationBuilder.DropIndex(
                name: "IX_DocumentosDB_TipoContaId",
                table: "DocumentosDB");

            migrationBuilder.DropIndex(
                name: "IX_DocumentosDB_UsuarioId",
                table: "DocumentosDB");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ComprovantesDB",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "DocumentoId",
                table: "ComprovantesDB",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_ComprovantesDB_DocumentoId",
                table: "ComprovantesDB",
                column: "DocumentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComprovantesDB_DocumentosDB_DocumentoId",
                table: "ComprovantesDB",
                column: "DocumentoId",
                principalTable: "DocumentosDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComprovantesDB_DocumentosDB_DocumentoId",
                table: "ComprovantesDB");

            migrationBuilder.DropIndex(
                name: "IX_DocumentosDB_FormaPagamentoId",
                table: "DocumentosDB");

            migrationBuilder.DropIndex(
                name: "IX_DocumentosDB_TipoContaId",
                table: "DocumentosDB");

            migrationBuilder.DropIndex(
                name: "IX_DocumentosDB_UsuarioId",
                table: "DocumentosDB");

            migrationBuilder.DropIndex(
                name: "IX_ComprovantesDB_DocumentoId",
                table: "ComprovantesDB");

            migrationBuilder.DropColumn(
                name: "DocumentoId",
                table: "ComprovantesDB");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ComprovantesDB",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ComprovantesDB_DocumentosDB_Id",
                table: "ComprovantesDB",
                column: "Id",
                principalTable: "DocumentosDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
