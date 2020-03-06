using Microsoft.EntityFrameworkCore.Migrations;

namespace Simchas.Data.Migrations
{
    public partial class UpdateContribution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlwaysInclude",
                table: "Contributors");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Contributions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AlwaysInclude",
                table: "Contributors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Contributions",
                nullable: false,
                defaultValue: 0);
        }
    }
}
