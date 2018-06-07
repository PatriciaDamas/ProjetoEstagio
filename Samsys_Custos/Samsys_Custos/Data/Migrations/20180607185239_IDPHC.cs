using Microsoft.EntityFrameworkCore.Migrations;

namespace Samsys_Custos.Data.Migrations
{
    public partial class IDPHC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CUSTO_DADOS_PHC_id_phc",
                table: "CUSTO");

            migrationBuilder.DropIndex(
                name: "IX_CUSTO_id_phc",
                table: "CUSTO");

            migrationBuilder.DropColumn(
                name: "id_phc",
                table: "CUSTO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_phc",
                table: "CUSTO",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CUSTO_id_phc",
                table: "CUSTO",
                column: "id_phc");

            migrationBuilder.AddForeignKey(
                name: "FK_CUSTO_DADOS_PHC_id_phc",
                table: "CUSTO",
                column: "id_phc",
                principalTable: "DADOS_PHC",
                principalColumn: "id_phc",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
