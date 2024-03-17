using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiMinhasFinancas.Migrations
{
    public partial class TabelaComprovantes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormasPgto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Valor = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormasPgto", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "DocumentosDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Valor = table.Column<double>(type: "double precision", nullable: false),
                    DataDocumento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    QtdParcelas = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CodigoMeta = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentosDB_FormasPgto_Id",
                        column: x => x.Id,
                        principalTable: "FormasPgto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentosDB_TipoContas_Id",
                        column: x => x.Id,
                        principalTable: "TipoContas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentosDB_Usuarios_Id",
                        column: x => x.Id,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comprovantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    TituloArquivo = table.Column<string>(type: "text", nullable: false),
                    CaminhoArquivo = table.Column<string>(type: "text", nullable: false),
                    TipoComprovante = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comprovantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comprovantes_DocumentosDB_Id",
                        column: x => x.Id,
                        principalTable: "DocumentosDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comprovantes");

            migrationBuilder.DropTable(
                name: "DocumentosDB");

            migrationBuilder.DropTable(
                name: "FormasPgto");

            migrationBuilder.DropTable(
                name: "TipoContas");
        }
    }
}
