using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoClinic.Profiles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsLinkedToAccount = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    PhotoPath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patient_FirstName_LastName_DateOfBirth",
                table: "Patient",
                columns: new[] { "FirstName", "LastName", "DateOfBirth" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patient");
        }
    }
}
