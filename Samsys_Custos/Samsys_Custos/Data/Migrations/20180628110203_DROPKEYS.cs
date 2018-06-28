using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Samsys_Custos.Data.Migrations
{
    public partial class DROPKEYS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATRIBUICAO_COLABORADOR_id_colaborador",
                table: "ATRIBUICAO");

            migrationBuilder.DropForeignKey(
                name: "FK_CUSTO_COLABORADOR_id_colaborador",
                table: "CUSTO");

            migrationBuilder.DropIndex(
                name: "IX_EQUIPA_id_lider",
                table: "EQUIPA");

            migrationBuilder.DropIndex(
                name: "IX_CUSTO_id_colaborador",
                table: "CUSTO");

            migrationBuilder.DropIndex(
                name: "IX_ATRIBUICAO_id_colaborador",
                table: "ATRIBUICAO");

     

            migrationBuilder.DropColumn(
                name: "id_colaborador",
                table: "CUSTO");

            migrationBuilder.DropColumn(
                name: "id_colaborador",
                table: "ATRIBUICAO");

            migrationBuilder.CreateIndex(
                name: "IX_EQUIPA_id_lider",
                table: "EQUIPA",
                column: "id_lider");

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIA_id_pai",
                table: "CATEGORIA",
                column: "id_pai");

            migrationBuilder.AddForeignKey(
                name: "FK_CATEGORIA_CATEGORIA_id_pai",
                table: "CATEGORIA",
                column: "id_pai",
                principalTable: "CATEGORIA",
                principalColumn: "id_categoria",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CATEGORIA_CATEGORIA_id_pai",
                table: "CATEGORIA");

            migrationBuilder.DropIndex(
                name: "IX_EQUIPA_id_lider",
                table: "EQUIPA");

            migrationBuilder.DropIndex(
                name: "IX_CATEGORIA_id_pai",
                table: "CATEGORIA");

           

            migrationBuilder.AddColumn<int>(
                name: "id_colaborador",
                table: "CUSTO",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_ATRIBUICAO_id_colaborador",
                table: "ATRIBUICAO",
                column: "id_colaborador");

            migrationBuilder.CreateIndex(
                name: "IX_COLABORADOR_PERMISSAO_id_colaborador",
                table: "COLABORADOR_PERMISSAO",
                column: "id_colaborador");


            migrationBuilder.AddForeignKey(
                name: "FK_ATRIBUICAO_COLABORADOR_id_colaborador",
                table: "ATRIBUICAO",
                column: "id_colaborador",
                principalTable: "COLABORADOR",
                principalColumn: "id_colaborador",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CUSTO_COLABORADOR_id_colaborador",
                table: "CUSTO",
                column: "id_colaborador",
                principalTable: "COLABORADOR",
                principalColumn: "id_colaborador",
                onDelete: ReferentialAction.Cascade);

          
        }
    }
}
