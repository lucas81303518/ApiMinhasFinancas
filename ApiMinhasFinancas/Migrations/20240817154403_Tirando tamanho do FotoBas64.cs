using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMinhasFinancas.Migrations
{
    public partial class TirandotamanhodoFotoBas64 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FotoBase64",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1048576)",
                oldMaxLength: 1048576);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FotoBase64",
                table: "AspNetUsers",
                type: "character varying(1048576)",
                maxLength: 1048576,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
