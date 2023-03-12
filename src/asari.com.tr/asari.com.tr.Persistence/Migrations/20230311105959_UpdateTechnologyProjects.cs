using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asari.com.tr.Persistence.Migrations
{
    public partial class UpdateTechnologyProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TecgnologyProjects");

            migrationBuilder.CreateTable(
                name: "TechnologyProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechnologyId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnologyProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnologyProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnologyProjects_Technologies_TechnologyId",
                        column: x => x.TechnologyId,
                        principalTable: "Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnologyProjects_ProjectId",
                table: "TechnologyProjects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnologyProjects_TechnologyId",
                table: "TechnologyProjects",
                column: "TechnologyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechnologyProjects");

            migrationBuilder.CreateTable(
                name: "TecgnologyProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    TechnologyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TecgnologyProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TecgnologyProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TecgnologyProjects_Technologies_TechnologyId",
                        column: x => x.TechnologyId,
                        principalTable: "Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TecgnologyProjects_ProjectId",
                table: "TecgnologyProjects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TecgnologyProjects_TechnologyId",
                table: "TecgnologyProjects",
                column: "TechnologyId");
        }
    }
}
