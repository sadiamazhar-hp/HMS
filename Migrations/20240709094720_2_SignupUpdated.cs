using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace V._3._0.Migrations
{
    /// <inheritdoc />
    public partial class _2_SignupUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "HospitalUser");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "HospitalUser",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "SignupId",
                table: "Patients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Roles",
                table: "HospitalUser",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "HospitalUser");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "HospitalUser",
                newName: "Location");

            migrationBuilder.AlterColumn<int>(
                name: "SignupId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "HospitalUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
