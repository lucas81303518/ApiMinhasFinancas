using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMinhasFinancas.Migrations
{
    public partial class corrijindonomecampometa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorObjectivo",
                table: "MetasDB",
                newName: "ValorObjetivo");           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorObjetivo",
                table: "MetasDB",
                newName: "ValorObjectivo");           
        }
    }
}
