﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Samsys_Custos.Data.Migrations
{
    public partial class userv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AlterColumn<int>(
                name: "Segsocial",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Segsocial",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int));

           
        }
    }
}
