using Microsoft.EntityFrameworkCore.Migrations;

namespace Samsys_Custos.Data.Migrations
{
    public partial class ADDKEYSNEW : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_lider",
                table: "EQUIPA",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id_colaborador",
                table: "CUSTO",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "id_equipa",
                table: "COLABORADOR",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_colaborador",
                table: "ATRIBUICAO",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EQUIPA_id_lider",
                table: "EQUIPA",
                column: "id_lider",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CUSTO_id_colaborador",
                table: "CUSTO",
                column: "id_colaborador");

            migrationBuilder.CreateIndex(
                name: "IX_COLABORADOR_id_equipa",
                table: "COLABORADOR",
                column: "id_equipa");

            migrationBuilder.CreateIndex(
                name: "IX_ATRIBUICAO_id_colaborador",
                table: "ATRIBUICAO",
                column: "id_colaborador");

            migrationBuilder.AddForeignKey(
                name: "FK_ATRIBUICAO_COLABORADOR_id_colaborador",
                table: "ATRIBUICAO",
                column: "id_colaborador",
                principalTable: "COLABORADOR",
                principalColumn: "id_colaborador",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_COLABORADOR_EQUIPA_id_equipa",
                table: "COLABORADOR",
                column: "id_equipa",
                principalTable: "EQUIPA",
                principalColumn: "id_equipa",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CUSTO_COLABORADOR_id_colaborador",
                table: "CUSTO",
                column: "id_colaborador",
                principalTable: "COLABORADOR",
                principalColumn: "id_colaborador",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EQUIPA_COLABORADOR_id_lider",
                table: "EQUIPA",
                column: "id_lider",
                principalTable: "COLABORADOR",
                principalColumn: "id_colaborador",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATRIBUICAO_COLABORADOR_id_colaborador",
                table: "ATRIBUICAO");

            migrationBuilder.DropForeignKey(
                name: "FK_COLABORADOR_EQUIPA_id_equipa",
                table: "COLABORADOR");

            migrationBuilder.DropForeignKey(
                name: "FK_CUSTO_COLABORADOR_id_colaborador",
                table: "CUSTO");

            migrationBuilder.DropForeignKey(
                name: "FK_EQUIPA_COLABORADOR_id_lider",
                table: "EQUIPA");

            migrationBuilder.DropIndex(
                name: "IX_EQUIPA_id_lider",
                table: "EQUIPA");

            migrationBuilder.DropIndex(
                name: "IX_CUSTO_id_colaborador",
                table: "CUSTO");

            migrationBuilder.DropIndex(
                name: "IX_COLABORADOR_id_equipa",
                table: "COLABORADOR");

            migrationBuilder.DropIndex(
                name: "IX_ATRIBUICAO_id_colaborador",
                table: "ATRIBUICAO");

            migrationBuilder.DropColumn(
                name: "id_lider",
                table: "EQUIPA");

            migrationBuilder.DropColumn(
                name: "id_colaborador",
                table: "CUSTO");

            migrationBuilder.DropColumn(
                name: "id_equipa",
                table: "COLABORADOR");

            migrationBuilder.DropColumn(
                name: "id_colaborador",
                table: "ATRIBUICAO");
        }
    }
}
