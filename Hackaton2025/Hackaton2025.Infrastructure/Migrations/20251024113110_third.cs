using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackaton2025.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PastProcedureJuries_PastProcedures_PastProcedureId",
                table: "PastProcedureJuries");

            migrationBuilder.DropForeignKey(
                name: "FK_PastProcedureJuryMembers_PastProcedureJuries_PastProcedureJuryId1",
                table: "PastProcedureJuryMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_PastProcedureJuryMembers_Teachers_TeacherId1",
                table: "PastProcedureJuryMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Universities_UniversityId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_UniversityFaculties_UniversityFacultyId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_UniversityFacultyId",
                table: "Teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PastProcedureJuryMembers",
                table: "PastProcedureJuryMembers");

            migrationBuilder.DropIndex(
                name: "IX_PastProcedureJuryMembers_TeacherId1",
                table: "PastProcedureJuryMembers");

            migrationBuilder.DropColumn(
                name: "UniversityFacultyId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "PastProcedureJuryMembers");

            migrationBuilder.RenameColumn(
                name: "PastProcedureJuryId1",
                table: "PastProcedureJuryMembers",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_PastProcedureJuryMembers_PastProcedureJuryId1",
                table: "PastProcedureJuryMembers",
                newName: "IX_PastProcedureJuryMembers_TeacherId");

            migrationBuilder.AlterColumn<string>(
                name: "UniversityFactultyId",
                table: "Teachers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Teachers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Teachers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Teachers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "PastProcedures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PastProcedureJuryMembers",
                table: "PastProcedureJuryMembers",
                columns: new[] { "PastProcedureJuryId", "TeacherId" });

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_LastName_FirstName",
                table: "Teachers",
                columns: new[] { "LastName", "FirstName" });

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_UniversityFactultyId",
                table: "Teachers",
                column: "UniversityFactultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PastProcedureJuries_PastProcedures_PastProcedureId",
                table: "PastProcedureJuries",
                column: "PastProcedureId",
                principalTable: "PastProcedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PastProcedureJuryMembers_PastProcedureJuries_PastProcedureJuryId",
                table: "PastProcedureJuryMembers",
                column: "PastProcedureJuryId",
                principalTable: "PastProcedureJuries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PastProcedureJuryMembers_Teachers_TeacherId",
                table: "PastProcedureJuryMembers",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Universities_UniversityId",
                table: "Teachers",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_UniversityFaculties_UniversityFactultyId",
                table: "Teachers",
                column: "UniversityFactultyId",
                principalTable: "UniversityFaculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PastProcedureJuries_PastProcedures_PastProcedureId",
                table: "PastProcedureJuries");

            migrationBuilder.DropForeignKey(
                name: "FK_PastProcedureJuryMembers_PastProcedureJuries_PastProcedureJuryId",
                table: "PastProcedureJuryMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_PastProcedureJuryMembers_Teachers_TeacherId",
                table: "PastProcedureJuryMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Universities_UniversityId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_UniversityFaculties_UniversityFactultyId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_LastName_FirstName",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_UniversityFactultyId",
                table: "Teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PastProcedureJuryMembers",
                table: "PastProcedureJuryMembers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "PastProcedures");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "PastProcedureJuryMembers",
                newName: "PastProcedureJuryId1");

            migrationBuilder.RenameIndex(
                name: "IX_PastProcedureJuryMembers_TeacherId",
                table: "PastProcedureJuryMembers",
                newName: "IX_PastProcedureJuryMembers_PastProcedureJuryId1");

            migrationBuilder.AlterColumn<string>(
                name: "UniversityFactultyId",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Title",
                table: "Teachers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "UniversityFacultyId",
                table: "Teachers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherId1",
                table: "PastProcedureJuryMembers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PastProcedureJuryMembers",
                table: "PastProcedureJuryMembers",
                column: "PastProcedureJuryId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_UniversityFacultyId",
                table: "Teachers",
                column: "UniversityFacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_PastProcedureJuryMembers_TeacherId1",
                table: "PastProcedureJuryMembers",
                column: "TeacherId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PastProcedureJuries_PastProcedures_PastProcedureId",
                table: "PastProcedureJuries",
                column: "PastProcedureId",
                principalTable: "PastProcedures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PastProcedureJuryMembers_PastProcedureJuries_PastProcedureJuryId1",
                table: "PastProcedureJuryMembers",
                column: "PastProcedureJuryId1",
                principalTable: "PastProcedureJuries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PastProcedureJuryMembers_Teachers_TeacherId1",
                table: "PastProcedureJuryMembers",
                column: "TeacherId1",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Universities_UniversityId",
                table: "Teachers",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_UniversityFaculties_UniversityFacultyId",
                table: "Teachers",
                column: "UniversityFacultyId",
                principalTable: "UniversityFaculties",
                principalColumn: "Id");
        }
    }
}
