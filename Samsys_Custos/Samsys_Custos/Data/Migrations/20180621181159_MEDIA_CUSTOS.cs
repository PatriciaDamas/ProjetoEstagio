using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Samsys_Custos.Data.Migrations
{
    public partial class MEDIA_CUSTOS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MEDIA_CUSTOS",
                columns: table => new
                {
                    id_media = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_equipa = table.Column<string>(nullable: true),
                    nome = table.Column<string>(nullable: true),
                    qtd_colaboradores = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    mes = table.Column<string>(nullable: true),
                    ano = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDIA_CUSTOS", x => x.id_media);
                    table.ForeignKey(
                        name: "FK_MEDIA_CUSTOS_EQUIPA_id_equipa",
                        column: x => x.id_equipa,
                        principalTable: "EQUIPA",
                        principalColumn: "id_equipa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MEDIA_CUSTOS_id_equipa",
                table: "MEDIA_CUSTOS",
                column: "id_equipa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MEDIA_CUSTOS");
        }
    }
}
