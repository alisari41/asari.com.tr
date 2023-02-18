﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using asari.com.tr.Persistence.Contexts;

#nullable disable

namespace asari.com.tr.Persistence.Migrations
{
    [DbContext(typeof(BaseDbContext))]
    [Migration("20230218155121_AddedJwtMigration")]
    partial class AddedJwtMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("asari.com.tr.Domain.Entities.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ActivityAndCommunity")
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("ActivityAndCommunity");

                    b.Property<double>("Degree")
                        .HasColumnType("float")
                        .HasColumnName("Degree");

                    b.Property<string>("Description")
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Description");

                    b.Property<DateTime?>("EndDateOrExcepted")
                        .HasColumnType("datetime2")
                        .HasColumnName("EndDateOrExcepted");

                    b.Property<string>("FieldOfStudy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR(100)")
                        .HasColumnName("FieldOfStudy");

                    b.Property<string>("Grade")
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR(500)")
                        .HasColumnName("Grade");

                    b.Property<string>("MediaUrl")
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("MediaUrl");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR(500)")
                        .HasColumnName("Name");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("StartDate");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Educations", (string)null);
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.EducationSkill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EducationId")
                        .HasColumnType("int")
                        .HasColumnName("EducationId");

                    b.Property<int>("SkillId")
                        .HasColumnType("int")
                        .HasColumnName("SkillId");

                    b.HasKey("Id");

                    b.HasIndex("EducationId");

                    b.HasIndex("SkillId");

                    b.ToTable("EducationSkills", (string)null);
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.Experience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("NVARCHAR(250)")
                        .HasColumnName("CompanyName");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Description");

                    b.Property<string>("EmploymentType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR(100)")
                        .HasColumnName("EmploymentType");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("EndDate");

                    b.Property<string>("Industry")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR(100)")
                        .HasColumnName("Industry");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("NVARCHAR(250)")
                        .HasColumnName("Location");

                    b.Property<string>("ProfileHeadline")
                        .HasMaxLength(250)
                        .HasColumnType("NVARCHAR(250)")
                        .HasColumnName("ProfileHeadline");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("StartDate");

                    b.Property<bool>("Statu")
                        .HasColumnType("bit")
                        .HasColumnName("Statu");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR(500)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("Experiences", (string)null);
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.ExperienceSkill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ExperienceId")
                        .HasColumnType("int")
                        .HasColumnName("ExperienceId");

                    b.Property<int>("SkillId")
                        .HasColumnType("int")
                        .HasColumnName("SkillId");

                    b.HasKey("Id");

                    b.HasIndex("ExperienceId");

                    b.HasIndex("SkillId");

                    b.ToTable("ExperienceSkills", (string)null);
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.LicenseAndCertification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CredentialId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("CredentialId");

                    b.Property<string>("CredentialUrl")
                        .IsRequired()
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("CredentialUrl");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ExpirationDate");

                    b.Property<string>("ImagegUrl")
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("ImagegUrl");

                    b.Property<DateTime?>("IssueDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("IssueDate");

                    b.Property<string>("IssuingOrganization")
                        .IsRequired()
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("IssuingOrganization");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR(500)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name", "CredentialId", "CredentialUrl");

                    b.ToTable("LicensesAndCertifications", (string)null);
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.LicenseAndCertificationSkill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LicenseAndCertificationId")
                        .HasColumnType("int")
                        .HasColumnName("LicenseAndCertificationId");

                    b.Property<int>("SkillId")
                        .HasColumnType("int")
                        .HasColumnName("SkillId");

                    b.HasKey("Id");

                    b.HasIndex("LicenseAndCertificationId");

                    b.HasIndex("SkillId");

                    b.ToTable("LicenseAndCertificationSkills", (string)null);
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.ProgrammingLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("ProgrammingLanguages", (string)null);
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.ProgrammingLanguageTechnology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR(100)")
                        .HasColumnName("Name");

                    b.Property<int?>("ProgrammingLanguageId")
                        .HasColumnType("int")
                        .HasColumnName("ProgrammingLanguageId1");

                    b.Property<int>("ProgrramingLanguageId")
                        .HasColumnType("int")
                        .HasColumnName("ProgrammingLanguageId");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.HasIndex("ProgrammingLanguageId");

                    b.ToTable("ProgrammingLanguageTechnologies", (string)null);
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Content");

                    b.Property<DateTime?>("CreateDate")
                        .IsRequired()
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Description");

                    b.Property<string>("FolderUrl")
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("FolderUrl");

                    b.Property<string>("GithubLink")
                        .IsRequired()
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("GithubLink");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("ImageUrl");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR(200)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.HasAlternateKey("Title", "GithubLink");

                    b.ToTable("Projects", (string)null);
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.ProjectProgrammingLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ProgrammingLanguageId")
                        .HasColumnType("int")
                        .HasColumnName("ProgrammingLanguageId");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProgrammingLanguageId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectProgrammingLanguages", (string)null);
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.ProjectSkill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("ProjectId");

                    b.Property<int>("SkillId")
                        .HasColumnType("int")
                        .HasColumnName("SkillId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SkillId");

                    b.ToTable("ProjectSkills", (string)null);
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR(200)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Skills", (string)null);
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.TecgnologyProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("ProjectId");

                    b.Property<int>("TechnologyId")
                        .HasColumnType("int")
                        .HasColumnName("TechnologyId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("TecgnologyProjects", (string)null);
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.Technology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Content");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Description");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("ImageUrl");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("NVARCHAR(250)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("Technologies", (string)null);
                });

            modelBuilder.Entity("Core.Security.Entities.OperationClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("OperationClaims", (string)null);
                });

            modelBuilder.Entity("Core.Security.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<string>("CreatedByIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CreatedByIp");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2")
                        .HasColumnName("Expires");

                    b.Property<string>("ReasonRevoked")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ReasonRevoked");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ReplacedByToken");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime2")
                        .HasColumnName("Revoked");

                    b.Property<string>("RevokedByIp")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("RevokedByIp");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Token");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens", (string)null);
                });

            modelBuilder.Entity("Core.Security.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AuthenticatorType")
                        .HasColumnType("int")
                        .HasColumnName("AuthenticatorType");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LastName");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("PasswordHash");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("PasswordSalt");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("Status");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Core.Security.Entities.UserOperationClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("OperationClaimId")
                        .HasColumnType("int")
                        .HasColumnName("OperationClaimId");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("OperationClaimId");

                    b.HasIndex("UserId");

                    b.ToTable("UserOperationClaims", (string)null);
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.EducationSkill", b =>
                {
                    b.HasOne("asari.com.tr.Domain.Entities.Education", "Education")
                        .WithMany("EducationSkills")
                        .HasForeignKey("EducationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("asari.com.tr.Domain.Entities.Skill", "Skill")
                        .WithMany("EducationSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Education");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.ExperienceSkill", b =>
                {
                    b.HasOne("asari.com.tr.Domain.Entities.Experience", "Experience")
                        .WithMany("ExperienceSkills")
                        .HasForeignKey("ExperienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("asari.com.tr.Domain.Entities.Skill", "Skill")
                        .WithMany("ExperienceSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Experience");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.LicenseAndCertificationSkill", b =>
                {
                    b.HasOne("asari.com.tr.Domain.Entities.LicenseAndCertification", "LicenseAndCertification")
                        .WithMany("LicenseAndCertificationSkills")
                        .HasForeignKey("LicenseAndCertificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("asari.com.tr.Domain.Entities.Skill", "Skill")
                        .WithMany("LicenseAndCertificationSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LicenseAndCertification");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.ProgrammingLanguageTechnology", b =>
                {
                    b.HasOne("asari.com.tr.Domain.Entities.ProgrammingLanguage", "ProgrammingLanguage")
                        .WithMany("ProgrammingLanguageTechnologies")
                        .HasForeignKey("ProgrammingLanguageId");

                    b.Navigation("ProgrammingLanguage");
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.ProjectProgrammingLanguage", b =>
                {
                    b.HasOne("asari.com.tr.Domain.Entities.ProgrammingLanguage", "ProgrammingLanguage")
                        .WithMany("ProjectProgrammingLanguages")
                        .HasForeignKey("ProgrammingLanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("asari.com.tr.Domain.Entities.Project", "Project")
                        .WithMany("ProjectProgrammingLanguages")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProgrammingLanguage");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.ProjectSkill", b =>
                {
                    b.HasOne("asari.com.tr.Domain.Entities.Project", "Project")
                        .WithMany("ProjectSkills")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("asari.com.tr.Domain.Entities.Skill", "Skill")
                        .WithMany("ProjectSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.TecgnologyProject", b =>
                {
                    b.HasOne("asari.com.tr.Domain.Entities.Project", "Project")
                        .WithMany("TecgnologyProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("asari.com.tr.Domain.Entities.Technology", "Technology")
                        .WithMany("TecgnologyProjects")
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Technology");
                });

            modelBuilder.Entity("Core.Security.Entities.RefreshToken", b =>
                {
                    b.HasOne("Core.Security.Entities.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Security.Entities.UserOperationClaim", b =>
                {
                    b.HasOne("Core.Security.Entities.OperationClaim", "OperationClaim")
                        .WithMany("UserOperationClaims")
                        .HasForeignKey("OperationClaimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Security.Entities.User", "User")
                        .WithMany("UserOperationClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OperationClaim");

                    b.Navigation("User");
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.Education", b =>
                {
                    b.Navigation("EducationSkills");
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.Experience", b =>
                {
                    b.Navigation("ExperienceSkills");
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.LicenseAndCertification", b =>
                {
                    b.Navigation("LicenseAndCertificationSkills");
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.ProgrammingLanguage", b =>
                {
                    b.Navigation("ProgrammingLanguageTechnologies");

                    b.Navigation("ProjectProgrammingLanguages");
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.Project", b =>
                {
                    b.Navigation("ProjectProgrammingLanguages");

                    b.Navigation("ProjectSkills");

                    b.Navigation("TecgnologyProjects");
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.Skill", b =>
                {
                    b.Navigation("EducationSkills");

                    b.Navigation("ExperienceSkills");

                    b.Navigation("LicenseAndCertificationSkills");

                    b.Navigation("ProjectSkills");
                });

            modelBuilder.Entity("asari.com.tr.Domain.Entities.Technology", b =>
                {
                    b.Navigation("TecgnologyProjects");
                });

            modelBuilder.Entity("Core.Security.Entities.OperationClaim", b =>
                {
                    b.Navigation("UserOperationClaims");
                });

            modelBuilder.Entity("Core.Security.Entities.User", b =>
                {
                    b.Navigation("RefreshTokens");

                    b.Navigation("UserOperationClaims");
                });
#pragma warning restore 612, 618
        }
    }
}
