using Microsoft.EntityFrameworkCore.Migrations;

namespace Samsys_Custos.Data.Migrations
{
    public partial class salariocusto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SALARIO_CUSTO_id_custo",
                table: "SALARIO");

            migrationBuilder.AlterColumn<int>(
                name: "id_custo",
                table: "SALARIO",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SALARIO_CUSTO_id_custo",
                table: "SALARIO",
                column: "id_custo",
                principalTable: "CUSTO",
                principalColumn: "id_custo",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SALARIO_CUSTO_id_custo",
                table: "SALARIO");

            migrationBuilder.AlterColumn<int>(
                name: "id_custo",
                table: "SALARIO",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_SALARIO_CUSTO_id_custo",
                table: "SALARIO",
                column: "id_custo",
                principalTable: "CUSTO",
                principalColumn: "id_custo",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
