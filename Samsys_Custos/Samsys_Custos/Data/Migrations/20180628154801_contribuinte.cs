using Microsoft.EntityFrameworkCore.Migrations;

namespace Samsys_Custos.Data.Migrations
{
    public partial class contribuinte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Segsocial",
                table: "AspNetUsers",
                newName: "contribuinte");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "contribuinte",
                table: "AspNetUsers",
                newName: "Segsocial");
        }
    }
}
