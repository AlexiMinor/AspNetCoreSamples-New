using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstMvcApp.Data.Migrations
{
    public partial class Rate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "PositivityRate",
                table: "Articles",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositivityRate",
                table: "Articles");
        }
    }
}
