using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationCore.Repository.Migrations
{
    public partial class Migration2705 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Resultat",
                table: "Parties",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resultat",
                table: "Parties");
        }
    }
}
