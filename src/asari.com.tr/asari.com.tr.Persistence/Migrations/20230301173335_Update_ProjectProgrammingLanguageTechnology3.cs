using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asari.com.tr.Persistence.Migrations
{
    public partial class Update_ProjectProgrammingLanguageTechnology3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProgrammingLanguageTechnologies_ProgrammingLanguages_ProgrammingLanguageId",
                table: "ProjectProgrammingLanguageTechnologies");

            migrationBuilder.DropIndex(
                name: "IX_ProjectProgrammingLanguageTechnologies_ProgrammingLanguageId",
                table: "ProjectProgrammingLanguageTechnologies");

            migrationBuilder.DropColumn(
                name: "ProgrammingLanguageId",
                table: "ProjectProgrammingLanguageTechnologies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProgrammingLanguageId",
                table: "ProjectProgrammingLanguageTechnologies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgrammingLanguageTechnologies_ProgrammingLanguageId",
                table: "ProjectProgrammingLanguageTechnologies",
                column: "ProgrammingLanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProgrammingLanguageTechnologies_ProgrammingLanguages_ProgrammingLanguageId",
                table: "ProjectProgrammingLanguageTechnologies",
                column: "ProgrammingLanguageId",
                principalTable: "ProgrammingLanguages",
                principalColumn: "Id");
        }
    }
}
