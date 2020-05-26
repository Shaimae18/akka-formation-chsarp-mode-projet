using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationCore.Repository.Migrations
{
    public partial class Migration26052 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Joueurs_Parties_PartieId",
                table: "Joueurs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Parties_PartieId",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_PartieId",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Joueurs_PartieId",
                table: "Joueurs");

            migrationBuilder.DropColumn(
                name: "PartieId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "PartieId",
                table: "Joueurs");

            migrationBuilder.AddColumn<int>(
                name: "TourId",
                table: "Positions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DernierTourId",
                table: "Parties",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_TourId",
                table: "Positions",
                column: "TourId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Tours_TourId",
                table: "Positions",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_Tours_DernierTourId",
                table: "Parties");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Tours_TourId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_TourId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Parties_DernierTourId",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "TourId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "DernierTourId",
                table: "Parties");

            migrationBuilder.AddColumn<int>(
                name: "PartieId",
                table: "Tours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartieId",
                table: "Joueurs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tours_PartieId",
                table: "Tours",
                column: "PartieId");

            migrationBuilder.CreateIndex(
                name: "IX_Joueurs_PartieId",
                table: "Joueurs",
                column: "PartieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Joueurs_Parties_PartieId",
                table: "Joueurs",
                column: "PartieId",
                principalTable: "Parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Parties_PartieId",
                table: "Tours",
                column: "PartieId",
                principalTable: "Parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
