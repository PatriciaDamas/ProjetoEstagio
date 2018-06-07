using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Samsys_Custos.Data.Migrations
{
    public partial class dropPHC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DADOS_PHC");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DADOS_PHC",
                columns: table => new
                {
                    id_phc = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    custo_interno = table.Column<bool>(nullable: false),
                    id_fornecedor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DADOS_PHC", x => x.id_phc);
                    table.ForeignKey(
                        name: "FK_DADOS_PHC_FORNECEDOR_id_fornecedor",
                        column: x => x.id_fornecedor,
                        principalTable: "FORNECEDOR",
                        principalColumn: "id_fornecedor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DADOS_PHC_id_fornecedor",
                table: "DADOS_PHC",
                column: "id_fornecedor");
        }
    }
}
