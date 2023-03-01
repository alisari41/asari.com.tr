using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asari.com.tr.Persistence.Migrations
{
    public partial class Update_ProjectProgrammingLanguageTechnology : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectProgrammingLanguages");

            migrationBuilder.CreateTable(
                name: "ProjectProgrammingLanguageTechnologies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgrammingLanguageTechnologyId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ProgrammingLanguageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProgrammingLanguageTechnologies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectProgrammingLanguageTechnologies_ProgrammingLanguages_ProgrammingLanguageId",
                        column: x => x.ProgrammingLanguageId,
                        principalTable: "ProgrammingLanguages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectProgrammingLanguageTechnologies_ProgrammingLanguageTechnologies_ProgrammingLanguageTechnologyId",
                        column: x => x.ProgrammingLanguageTechnologyId,
                        principalTable: "ProgrammingLanguageTechnologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectProgrammingLanguageTechnologies_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgrammingLanguageTechnologies_ProgrammingLanguageId",
                table: "ProjectProgrammingLanguageTechnologies",
                column: "ProgrammingLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgrammingLanguageTechnologies_ProgrammingLanguageTechnologyId",
                table: "ProjectProgrammingLanguageTechnologies",
                column: "ProgrammingLanguageTechnologyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgrammingLanguageTechnologies_ProjectId",
                table: "ProjectProgrammingLanguageTechnologies",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectProgrammingLanguageTechnologies");

            migrationBuilder.CreateTable(
                name: "ProjectProgrammingLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgrammingLanguageTechnologyId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ProgrammingLanguageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProgrammingLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectProgrammingLanguages_ProgrammingLanguages_ProgrammingLanguageId",
                        column: x => x.ProgrammingLanguageId,
                        principalTable: "ProgrammingLanguages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectProgrammingLanguages_ProgrammingLanguageTechnologies_ProgrammingLanguageTechnologyId",
                        column: x => x.ProgrammingLanguageTechnologyId,
                        principalTable: "ProgrammingLanguageTechnologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectProgrammingLanguages_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgrammingLanguages_ProgrammingLanguageId",
                table: "ProjectProgrammingLanguages",
                column: "ProgrammingLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgrammingLanguages_ProgrammingLanguageTechnologyId",
                table: "ProjectProgrammingLanguages",
                column: "ProgrammingLanguageTechnologyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgrammingLanguages_ProjectId",
                table: "ProjectProgrammingLanguages",
                column: "ProjectId");
        }
    }
}
