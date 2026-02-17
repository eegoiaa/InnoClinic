using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoClinic.Services.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameLookupToReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SpecializationLookups",
                table: "SpecializationLookups");

            migrationBuilder.RenameTable(
                name: "SpecializationLookups",
                newName: "SpecializationReferences");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpecializationReferences",
                table: "SpecializationReferences",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SpecializationReferences",
                table: "SpecializationReferences");

            migrationBuilder.RenameTable(
                name: "SpecializationReferences",
                newName: "SpecializationLookups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpecializationLookups",
                table: "SpecializationLookups",
                column: "Id");
        }
    }
}
