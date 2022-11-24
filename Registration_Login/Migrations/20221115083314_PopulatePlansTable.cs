using Microsoft.EntityFrameworkCore.Migrations;

namespace Registration_Login.Migrations
{
    public partial class PopulatePlansTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(" Insert Plans values('Basic','299/Month','1.5 GB','Unlimited Calling','100 SMS Per Day','28 Days')");
            migrationBuilder.Sql(" Insert Plans values('Pro','666/3 Month','1.5 GB','Unlimited Calling','100 SMS Per Day','84 Days')");
            migrationBuilder.Sql(" Insert Plans values('Pro','1099/6 Month','2.0 GB','Unlimited Calling','150 SMS Per Day','168 Days')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
