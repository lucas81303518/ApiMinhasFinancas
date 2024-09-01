using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMinhasFinancas.Migrations
{
    public partial class AlteradoModeldeMetas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "MetasDB",
                newName: "ValorResultado");

            migrationBuilder.AddColumn<double>(
                name: "ValorObjectivo",
                table: "MetasDB",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorObjectivo",
                table: "MetasDB");

            migrationBuilder.RenameColumn(
                name: "ValorResultado",
                table: "MetasDB",
                newName: "Valor");
        }
    }
}
