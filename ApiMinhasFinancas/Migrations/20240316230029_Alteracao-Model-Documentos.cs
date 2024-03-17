using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiMinhasFinancas.Migrations
{
    public partial class AlteracaoModelDocumentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentosDB_FormasPgtoDB_Id",
                table: "DocumentosDB");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentosDB_TipoContasDB_Id",
                table: "DocumentosDB");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentosDB_UsuariosDB_Id",
                table: "DocumentosDB");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DocumentosDB",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "FormasPgtoDB",
                table: "DocumentosDB",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoContasDB",
                table: "DocumentosDB",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuariosDB",
                table: "DocumentosDB",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosDB_FormasPgtoDB",
                table: "DocumentosDB",
                column: "FormasPgtoDB");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosDB_TipoContasDB",
                table: "DocumentosDB",
                column: "TipoContasDB");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosDB_UsuariosDB",
                table: "DocumentosDB",
                column: "UsuariosDB");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_DocumentosDB_FormasPgtoDB",
                table: "DocumentosDB");

            migrationBuilder.DropIndex(
                name: "IX_DocumentosDB_TipoContasDB",
                table: "DocumentosDB");

            migrationBuilder.DropIndex(
                name: "IX_DocumentosDB_UsuariosDB",
                table: "DocumentosDB");

            migrationBuilder.DropColumn(
                name: "FormasPgtoDB",
                table: "DocumentosDB");

            migrationBuilder.DropColumn(
                name: "TipoContasDB",
                table: "DocumentosDB");

            migrationBuilder.DropColumn(
                name: "UsuariosDB",
                table: "DocumentosDB");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DocumentosDB",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentosDB_FormasPgtoDB_Id",
                table: "DocumentosDB",
                column: "Id",
                principalTable: "FormasPgtoDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentosDB_TipoContasDB_Id",
                table: "DocumentosDB",
                column: "Id",
                principalTable: "TipoContasDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentosDB_UsuariosDB_Id",
                table: "DocumentosDB",
                column: "Id",
                principalTable: "UsuariosDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
