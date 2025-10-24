using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackaton2025.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UniversityFaculties_Universities_UniversityId1",
                table: "UniversityFaculties");

            migrationBuilder.DropIndex(
                name: "IX_UniversityFaculties_UniversityId1",
                table: "UniversityFaculties");

            migrationBuilder.DropColumn(
                name: "UniversityId1",
                table: "UniversityFaculties");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UniversityFaculties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UniversityId",
                table: "UniversityFaculties",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityFaculties_UniversityId",
                table: "UniversityFaculties",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_UniversityFaculties_Universities_UniversityId",
                table: "UniversityFaculties",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UniversityFaculties_Universities_UniversityId",
                table: "UniversityFaculties");

            migrationBuilder.DropIndex(
                name: "IX_UniversityFaculties_UniversityId",
                table: "UniversityFaculties");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "UniversityFaculties");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "UniversityFaculties");

            migrationBuilder.AddColumn<string>(
                name: "UniversityId1",
                table: "UniversityFaculties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UniversityFaculties_UniversityId1",
                table: "UniversityFaculties",
                column: "UniversityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UniversityFaculties_Universities_UniversityId1",
                table: "UniversityFaculties",
                column: "UniversityId1",
                principalTable: "Universities",
                principalColumn: "Id");
        }
    }
}
