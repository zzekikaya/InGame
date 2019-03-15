using Microsoft.EntityFrameworkCore.Migrations;

namespace InGame.Infrastructure.Migrations
{
    public partial class InitialDbMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Cagetories_CategoryID",
                table: "SubCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cagetories",
                table: "Cagetories");

            migrationBuilder.RenameTable(
                name: "Cagetories",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categories_CategoryID",
                table: "SubCategories",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categories_CategoryID",
                table: "SubCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Cagetories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cagetories",
                table: "Cagetories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Cagetories_CategoryID",
                table: "SubCategories",
                column: "CategoryID",
                principalTable: "Cagetories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
