using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackaton2025.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PastProcedures",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PastProcedures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PastProcedureJuries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PastProcedureId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PastProcedureJuries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PastProcedureJuries_PastProcedures_PastProcedureId",
                        column: x => x.PastProcedureId,
                        principalTable: "PastProcedures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UniversityFaculties",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UniversityId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversityFaculties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UniversityFaculties_Universities_UniversityId1",
                        column: x => x.UniversityId1,
                        principalTable: "Universities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniversityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UniversityFactultyId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniversityFacultyId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Distance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SecondLastJuryMemberDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastJuryMemberDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teachers_UniversityFaculties_UniversityFacultyId",
                        column: x => x.UniversityFacultyId,
                        principalTable: "UniversityFaculties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PastProcedureJuryMembers",
                columns: table => new
                {
                    PastProcedureJuryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PastProcedureJuryId1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeacherId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PastProcedureJuryMembers", x => x.PastProcedureJuryId);
                    table.ForeignKey(
                        name: "FK_PastProcedureJuryMembers_PastProcedureJuries_PastProcedureJuryId1",
                        column: x => x.PastProcedureJuryId1,
                        principalTable: "PastProcedureJuries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PastProcedureJuryMembers_Teachers_TeacherId1",
                        column: x => x.TeacherId1,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PastProcedureJuries_PastProcedureId",
                table: "PastProcedureJuries",
                column: "PastProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_PastProcedureJuryMembers_PastProcedureJuryId1",
                table: "PastProcedureJuryMembers",
                column: "PastProcedureJuryId1");

            migrationBuilder.CreateIndex(
                name: "IX_PastProcedureJuryMembers_TeacherId1",
                table: "PastProcedureJuryMembers",
                column: "TeacherId1");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_UniversityFacultyId",
                table: "Teachers",
                column: "UniversityFacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_UniversityId",
                table: "Teachers",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityFaculties_UniversityId1",
                table: "UniversityFaculties",
                column: "UniversityId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PastProcedureJuryMembers");

            migrationBuilder.DropTable(
                name: "PastProcedureJuries");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "PastProcedures");

            migrationBuilder.DropTable(
                name: "UniversityFaculties");

            migrationBuilder.DropTable(
                name: "Universities");
        }
    }
}
