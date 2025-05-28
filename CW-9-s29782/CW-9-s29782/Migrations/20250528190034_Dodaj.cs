using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CW_9_s29782.Migrations
{
    /// <inheritdoc />
    public partial class Dodaj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthate",
                table: "Patient",
                newName: "Birthdate");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Patient",
                newName: "IdPatient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthdate",
                table: "Patient",
                newName: "Birthate");

            migrationBuilder.RenameColumn(
                name: "IdPatient",
                table: "Patient",
                newName: "PatientId");
        }
    }
}
