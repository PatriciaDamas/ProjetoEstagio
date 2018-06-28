using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Samsys_Custos.Data.Migrations
{
    public partial class DROPKEYS2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropTable(
                name: "COLABORADOR");

            migrationBuilder.DropIndex(
                name: "IX_EQUIPA_id_lider",
                table: "EQUIPA");

            migrationBuilder.DropColumn(
                name: "id_lider",
                table: "EQUIPA");

            migrationBuilder.CreateTable(
                name: "COLABORADOR",
                columns: table => new
                {
                    id_colaborador = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(nullable: true),
                    data_admissao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COLABORADOR", x => x.id_colaborador);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COLABORADOR");

            migrationBuilder.AddColumn<int>(
                name: "id_lider",
                table: "EQUIPA",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "COLABORADOR",
                columns: table => new
                {
                    id_colaborador = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    data_admissao = table.Column<DateTime>(nullable: false),
                    id_equipa = table.Column<string>(nullable: true),
                    nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COLABORADOR", x => x.id_colaborador);
                   
                });

            migrationBuilder.CreateIndex(
                name: "IX_EQUIPA_id_lider",
                table: "EQUIPA",
                column: "id_lider");

            migrationBuilder.CreateIndex(
                name: "IX_COLABORADOR_id_equipa",
                table: "COLABORADOR",
                column: "id_equipa");

            
        }
    }
}
