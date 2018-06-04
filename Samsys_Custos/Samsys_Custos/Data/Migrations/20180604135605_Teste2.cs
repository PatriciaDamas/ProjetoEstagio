using Microsoft.EntityFrameworkCore.Migrations;

namespace Samsys_Custos.Data.Migrations
{
    public partial class Teste2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_custo",
                table: "SALARIO",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SALARIO_id_custo",
                table: "SALARIO",
                column: "id_custo");

            migrationBuilder.AddForeignKey(
                name: "FK_SALARIO_CUSTO_id_custo",
                table: "SALARIO",
                column: "id_custo",
                principalTable: "CUSTO",
                principalColumn: "id_custo",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SALARIO_CUSTO_id_custo",
                table: "SALARIO");

            migrationBuilder.DropIndex(
                name: "IX_SALARIO_id_custo",
                table: "SALARIO");

            migrationBuilder.DropColumn(
                name: "id_custo",
                table: "SALARIO");
        }
    }
}
