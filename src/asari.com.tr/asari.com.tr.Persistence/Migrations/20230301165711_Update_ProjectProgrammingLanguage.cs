using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asari.com.tr.Persistence.Migrations
{
    public partial class Update_ProjectProgrammingLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProgrammingLanguages_ProgrammingLanguages_ProgrammingLanguageId",
                table: "ProjectProgrammingLanguages");

            migrationBuilder.AlterColumn<int>(
                name: "ProgrammingLanguageId",
                table: "ProjectProgrammingLanguages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProgrammingLanguageTechnologyId",
                table: "ProjectProgrammingLanguages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgrammingLanguages_ProgrammingLanguageTechnologyId",
                table: "ProjectProgrammingLanguages",
                column: "ProgrammingLanguageTechnologyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProgrammingLanguages_ProgrammingLanguages_ProgrammingLanguageId",
                table: "ProjectProgrammingLanguages",
                column: "ProgrammingLanguageId",
                principalTable: "ProgrammingLanguages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProgrammingLanguages_ProgrammingLanguageTechnologies_ProgrammingLanguageTechnologyId",
                table: "ProjectProgrammingLanguages",
                column: "ProgrammingLanguageTechnologyId",
                principalTable: "ProgrammingLanguageTechnologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProgrammingLanguages_ProgrammingLanguages_ProgrammingLanguageId",
                table: "ProjectProgrammingLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProgrammingLanguages_ProgrammingLanguageTechnologies_ProgrammingLanguageTechnologyId",
                table: "ProjectProgrammingLanguages");

            migrationBuilder.DropIndex(
                name: "IX_ProjectProgrammingLanguages_ProgrammingLanguageTechnologyId",
                table: "ProjectProgrammingLanguages");

            migrationBuilder.DropColumn(
                name: "ProgrammingLanguageTechnologyId",
                table: "ProjectProgrammingLanguages");

            migrationBuilder.AlterColumn<int>(
                name: "ProgrammingLanguageId",
                table: "ProjectProgrammingLanguages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProgrammingLanguages_ProgrammingLanguages_ProgrammingLanguageId",
                table: "ProjectProgrammingLanguages",
                column: "ProgrammingLanguageId",
                principalTable: "ProgrammingLanguages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
