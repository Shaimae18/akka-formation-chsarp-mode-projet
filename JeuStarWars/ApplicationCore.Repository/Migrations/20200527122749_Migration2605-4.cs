using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationCore.Repository.Migrations
{
    public partial class Migration26054 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_Tours_DernierTourId",
                table: "Parties");

            migrationBuilder.DropIndex(
                name: "IX_Parties_DernierTourId",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "DernierTourId",
                table: "Parties");

            migrationBuilder.AddColumn<int>(
                name: "PartieId",
                table: "Tours",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tours_PartieId",
                table: "Tours",
                column: "PartieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Parties_PartieId",
                table: "Tours",
                column: "PartieId",
                principalTable: "Parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Parties_PartieId",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_PartieId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "PartieId",
                table: "Tours");

            migrationBuilder.AddColumn<int>(
                name: "DernierTourId",
                table: "Parties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parties_DernierTourId",
                table: "Parties",
                column: "DernierTourId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_Tours_DernierTourId",
                table: "Parties",
                column: "DernierTourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
