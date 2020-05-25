using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationCore.Repository.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parametrages_Parties_PartieId",
                table: "Parametrages");

            migrationBuilder.DropIndex(
                name: "IX_Parametrages_PartieId",
                table: "Parametrages");

            migrationBuilder.DropColumn(
                name: "PartieId",
                table: "Parametrages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartieId",
                table: "Parametrages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parametrages_PartieId",
                table: "Parametrages",
                column: "PartieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parametrages_Parties_PartieId",
                table: "Parametrages",
                column: "PartieId",
                principalTable: "Parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
