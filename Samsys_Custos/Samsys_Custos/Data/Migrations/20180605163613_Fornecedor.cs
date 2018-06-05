using Microsoft.EntityFrameworkCore.Migrations;

namespace Samsys_Custos.Data.Migrations
{
    public partial class Fornecedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "id_fornecedor",
                table: "DADOS_PHC",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FORNECEDOR",
                columns: table => new
                {
                    id_fornecedor = table.Column<string>(nullable: false),
                    sugestao_categoria = table.Column<int>(nullable: true),
                    sugestao_custo = table.Column<bool>(nullable: true),
                    nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORNECEDOR", x => x.id_fornecedor);
                    table.ForeignKey(
                        name: "FK_FORNECEDOR_CATEGORIA_sugestao_categoria",
                        column: x => x.sugestao_categoria,
                        principalTable: "CATEGORIA",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DADOS_PHC_id_fornecedor",
                table: "DADOS_PHC",
                column: "id_fornecedor");

            migrationBuilder.CreateIndex(
                name: "IX_FORNECEDOR_sugestao_categoria",
                table: "FORNECEDOR",
                column: "sugestao_categoria");

            migrationBuilder.AddForeignKey(
                name: "FK_DADOS_PHC_FORNECEDOR_id_fornecedor",
                table: "DADOS_PHC",
                column: "id_fornecedor",
                principalTable: "FORNECEDOR",
                principalColumn: "id_fornecedor",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DADOS_PHC_FORNECEDOR_id_fornecedor",
                table: "DADOS_PHC");

            migrationBuilder.DropTable(
                name: "FORNECEDOR");

            migrationBuilder.DropIndex(
                name: "IX_DADOS_PHC_id_fornecedor",
                table: "DADOS_PHC");

            migrationBuilder.DropColumn(
                name: "id_fornecedor",
                table: "DADOS_PHC");
        }
    }
}
