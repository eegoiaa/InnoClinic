using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoClinic.Profiles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDoctorFullNameComputedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Doctors",
                type: "text",
                nullable: false,
                computedColumnSql: "\"LastName\" || ' ' || \"FirstName\" || ' ' || COALESCE(\"MiddleName\", '')",
                stored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Doctors");
        }
    }
}
