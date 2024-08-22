using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMinhasFinancas.Migrations
{
    public partial class AlterandoModelSaldoMensalparaSaldo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ano",
                table: "SaldoMensalDB");

            migrationBuilder.DropColumn(
                name: "Mes",
                table: "SaldoMensalDB");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ano",
                table: "SaldoMensalDB",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mes",
                table: "SaldoMensalDB",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
