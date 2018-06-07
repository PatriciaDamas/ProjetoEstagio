using Microsoft.EntityFrameworkCore.Migrations;

namespace Samsys_Custos.Data.Migrations
{
    public partial class foreignPHC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "id_phc",
                table: "CUSTO",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DADOS_PHC",
                columns: table => new
                {
                    id_phc = table.Column<string>(nullable: false),
                    id_fornecedor = table.Column<string>(nullable: true),
                    custo_interno = table.Column<bool>(nullable: false)
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
                name: "IX_CUSTO_id_phc",
                table: "CUSTO",
                column: "id_phc");

            migrationBuilder.CreateIndex(
                name: "IX_DADOS_PHC_id_fornecedor",
                table: "DADOS_PHC",
                column: "id_fornecedor");

            migrationBuilder.AddForeignKey(
                name: "FK_CUSTO_DADOS_PHC_id_phc",
                table: "CUSTO",
                column: "id_phc",
                principalTable: "DADOS_PHC",
                principalColumn: "id_phc",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CUSTO_DADOS_PHC_id_phc",
                table: "CUSTO");

            migrationBuilder.DropTable(
                name: "DADOS_PHC");

            migrationBuilder.DropIndex(
                name: "IX_CUSTO_id_phc",
                table: "CUSTO");

            migrationBuilder.DropColumn(
                name: "id_phc",
                table: "CUSTO");
        }
    }
}
