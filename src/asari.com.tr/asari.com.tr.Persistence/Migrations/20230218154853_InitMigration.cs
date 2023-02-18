using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asari.com.tr.Persistence.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(500)", maxLength: 500, nullable: false),
                    Degree = table.Column<double>(type: "float", nullable: false),
                    FieldOfStudy = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateOrExcepted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Grade = table.Column<string>(type: "NVARCHAR(500)", maxLength: 500, nullable: true),
                    ActivityAndCommunity = table.Column<string>(type: "NVARCHAR", nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR", nullable: true),
                    MediaUrl = table.Column<string>(type: "NVARCHAR", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                    table.UniqueConstraint("AK_Educations_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "NVARCHAR(500)", maxLength: 500, nullable: false),
                    EmploymentType = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    CompanyName = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: false),
                    Location = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: false),
                    Statu = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Industry = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR", nullable: false),
                    ProfileHeadline = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LicensesAndCertifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(500)", maxLength: 500, nullable: false),
                    IssuingOrganization = table.Column<string>(type: "NVARCHAR", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImagegUrl = table.Column<string>(type: "NVARCHAR", nullable: true),
                    CredentialId = table.Column<string>(type: "NVARCHAR", nullable: false),
                    CredentialUrl = table.Column<string>(type: "NVARCHAR", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicensesAndCertifications", x => x.Id);
                    table.UniqueConstraint("AK_LicensesAndCertifications_Name_CredentialId_CredentialUrl", x => new { x.Name, x.CredentialId, x.CredentialUrl });
                });

            migrationBuilder.CreateTable(
                name: "OperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammingLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingLanguages", x => x.Id);
                    table.UniqueConstraint("AK_ProgrammingLanguages_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR", nullable: false),
                    ImageUrl = table.Column<string>(type: "NVARCHAR", nullable: true),
                    Content = table.Column<string>(type: "NVARCHAR", nullable: false),
                    GithubLink = table.Column<string>(type: "NVARCHAR", nullable: false),
                    FolderUrl = table.Column<string>(type: "NVARCHAR", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.UniqueConstraint("AK_Projects_Title_GithubLink", x => new { x.Title, x.GithubLink });
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.UniqueConstraint("AK_Skills_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR", nullable: false),
                    ImageUrl = table.Column<string>(type: "NVARCHAR", nullable: true),
                    Content = table.Column<string>(type: "NVARCHAR", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    AuthenticatorType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammingLanguageTechnologies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgrammingLanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    ProgrammingLanguageId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingLanguageTechnologies", x => x.Id);
                    table.UniqueConstraint("AK_ProgrammingLanguageTechnologies_Name", x => x.Name);
                    table.ForeignKey(
                        name: "FK_ProgrammingLanguageTechnologies_ProgrammingLanguages_ProgrammingLanguageId1",
                        column: x => x.ProgrammingLanguageId1,
                        principalTable: "ProgrammingLanguages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectProgrammingLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgrammingLanguageId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProgrammingLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectProgrammingLanguages_ProgrammingLanguages_ProgrammingLanguageId",
                        column: x => x.ProgrammingLanguageId,
                        principalTable: "ProgrammingLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectProgrammingLanguages_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationSkills_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    ExperienceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienceSkills_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExperienceSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LicenseAndCertificationSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    LicenseAndCertificationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseAndCertificationSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicenseAndCertificationSkills_LicensesAndCertifications_LicenseAndCertificationId",
                        column: x => x.LicenseAndCertificationId,
                        principalTable: "LicensesAndCertifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LicenseAndCertificationSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectSkills_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TecgnologyProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechnologyId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonRevoked = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OperationClaimId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperationClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_OperationClaims_OperationClaimId",
                        column: x => x.OperationClaimId,
                        principalTable: "OperationClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationSkills_EducationId",
                table: "EducationSkills",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationSkills_SkillId",
                table: "EducationSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceSkills_ExperienceId",
                table: "ExperienceSkills",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceSkills_SkillId",
                table: "ExperienceSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseAndCertificationSkills_LicenseAndCertificationId",
                table: "LicenseAndCertificationSkills",
                column: "LicenseAndCertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseAndCertificationSkills_SkillId",
                table: "LicenseAndCertificationSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammingLanguageTechnologies_ProgrammingLanguageId1",
                table: "ProgrammingLanguageTechnologies",
                column: "ProgrammingLanguageId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgrammingLanguages_ProgrammingLanguageId",
                table: "ProjectProgrammingLanguages",
                column: "ProgrammingLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgrammingLanguages_ProjectId",
                table: "ProjectProgrammingLanguages",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSkills_ProjectId",
                table: "ProjectSkills",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSkills_SkillId",
                table: "ProjectSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TecgnologyProjects_ProjectId",
                table: "TecgnologyProjects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TecgnologyProjects_TechnologyId",
                table: "TecgnologyProjects",
                column: "TechnologyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_OperationClaimId",
                table: "UserOperationClaims",
                column: "OperationClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_UserId",
                table: "UserOperationClaims",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationSkills");

            migrationBuilder.DropTable(
                name: "ExperienceSkills");

            migrationBuilder.DropTable(
                name: "LicenseAndCertificationSkills");

            migrationBuilder.DropTable(
                name: "ProgrammingLanguageTechnologies");

            migrationBuilder.DropTable(
                name: "ProjectProgrammingLanguages");

            migrationBuilder.DropTable(
                name: "ProjectSkills");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "TecgnologyProjects");

            migrationBuilder.DropTable(
                name: "UserOperationClaims");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "LicensesAndCertifications");

            migrationBuilder.DropTable(
                name: "ProgrammingLanguages");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Technologies");

            migrationBuilder.DropTable(
                name: "OperationClaims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
