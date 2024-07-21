using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMinhasFinancas.Migrations
{
    public partial class AddAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UsuariosDB",
                columns: new[] { "Nome", "Email", "Senha" },
                values: new object[] { "Admin", "lucasferreira8130@gmail.com", "7a262443c7c0ddc9dddd94f494743d6c71348d4e475a9afb876267d3016fd69d" }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsuariosDB",
                keyColumn: "Email",
                keyValue: "lucasferreira8130@gmail.com"
            );
        }
    }
}
