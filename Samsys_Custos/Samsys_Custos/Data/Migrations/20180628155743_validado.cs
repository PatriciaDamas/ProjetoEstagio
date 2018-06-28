using Microsoft.EntityFrameworkCore.Migrations;

namespace Samsys_Custos.Data.Migrations
{
    public partial class validado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "validado",
                table: "DADOS_PHC",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "validado",
                table: "DADOS_PHC");
        }
    }
}
