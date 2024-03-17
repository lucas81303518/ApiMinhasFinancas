using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiMinhasFinancas.Migrations
{
    public partial class TabelaMetas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comprovantes_DocumentosDB_Id",
                table: "Comprovantes");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentosDB_FormasPgto_Id",
                table: "DocumentosDB");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentosDB_TipoContas_Id",
                table: "DocumentosDB");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentosDB_Usuarios_Id",
                table: "DocumentosDB");

            migrationBuilder.DropTable(
                name: "TipoContas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormasPgto",
                table: "FormasPgto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comprovantes",
                table: "Comprovantes");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "UsuariosDB");

            migrationBuilder.RenameTable(
                name: "FormasPgto",
                newName: "FormasPgtoDB");

            migrationBuilder.RenameTable(
                name: "Comprovantes",
                newName: "ComprovantesDB");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuariosDB",
                table: "UsuariosDB",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormasPgtoDB",
                table: "FormasPgtoDB",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComprovantesDB",
                table: "ComprovantesDB",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MetasDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Valor = table.Column<double>(type: "double precision", nullable: false),
                    DataInsercao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataPrevisao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetasDB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoContasDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomeConta = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoContasDB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransferenciasDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    ContaOrigem = table.Column<int>(type: "integer", nullable: false),
                    ContaDestino = table.Column<int>(type: "integer", nullable: false),
                    DataTransferencia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Valor = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferenciasDB", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ComprovantesDB_DocumentosDB_Id",
                table: "ComprovantesDB",
                column: "Id",
                principalTable: "DocumentosDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComprovantesDB_DocumentosDB_Id",
                table: "ComprovantesDB");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentosDB_FormasPgtoDB_Id",
                table: "DocumentosDB");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentosDB_TipoContasDB_Id",
                table: "DocumentosDB");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentosDB_UsuariosDB_Id",
                table: "DocumentosDB");

            migrationBuilder.DropTable(
                name: "MetasDB");

            migrationBuilder.DropTable(
                name: "TipoContasDB");

            migrationBuilder.DropTable(
                name: "TransferenciasDB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuariosDB",
                table: "UsuariosDB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormasPgtoDB",
                table: "FormasPgtoDB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComprovantesDB",
                table: "ComprovantesDB");

            migrationBuilder.RenameTable(
                name: "UsuariosDB",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "FormasPgtoDB",
                newName: "FormasPgto");

            migrationBuilder.RenameTable(
                name: "ComprovantesDB",
                newName: "Comprovantes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormasPgto",
                table: "FormasPgto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comprovantes",
                table: "Comprovantes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TipoContas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomeConta = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoContas", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comprovantes_DocumentosDB_Id",
                table: "Comprovantes",
                column: "Id",
                principalTable: "DocumentosDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentosDB_FormasPgto_Id",
                table: "DocumentosDB",
                column: "Id",
                principalTable: "FormasPgto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentosDB_TipoContas_Id",
                table: "DocumentosDB",
                column: "Id",
                principalTable: "TipoContas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentosDB_Usuarios_Id",
                table: "DocumentosDB",
                column: "Id",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
