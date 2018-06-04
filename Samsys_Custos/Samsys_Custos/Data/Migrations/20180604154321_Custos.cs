using Microsoft.EntityFrameworkCore.Migrations;

namespace Samsys_Custos.Data.Migrations
{
    public partial class Custos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CUSTO_SALARIO_id_salario",
                table: "CUSTO");

            migrationBuilder.DropIndex(
                name: "IX_CUSTO_id_salario",
                table: "CUSTO");

            migrationBuilder.DropColumn(
                name: "id_salario",
                table: "CUSTO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_salario",
                table: "CUSTO",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CUSTO_id_salario",
                table: "CUSTO",
                column: "id_salario");

            migrationBuilder.AddForeignKey(
                name: "FK_CUSTO_SALARIO_id_salario",
                table: "CUSTO",
                column: "id_salario",
                principalTable: "SALARIO",
                principalColumn: "id_salario",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
