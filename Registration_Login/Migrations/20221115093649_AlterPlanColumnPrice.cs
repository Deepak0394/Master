using Microsoft.EntityFrameworkCore.Migrations;

namespace Registration_Login.Migrations
{
    public partial class AlterPlanColumnPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricePerMonth",
                table: "Plans",
                newName: "PlanPrice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlanPrice",
                table: "Plans",
                newName: "PricePerMonth");
        }
    }
}
