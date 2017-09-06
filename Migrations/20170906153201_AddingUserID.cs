using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace todoAuth.Migrations
{
    public partial class AddingUserID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Todos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Todos",
                type: "int4",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_ApplicationUserId",
                table: "Todos",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_AspNetUsers_ApplicationUserId",
                table: "Todos",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_AspNetUsers_ApplicationUserId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_ApplicationUserId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Todos");
        }
    }
}
