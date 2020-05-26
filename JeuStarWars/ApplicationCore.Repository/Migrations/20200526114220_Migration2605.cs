using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationCore.Repository.Migrations
{
    public partial class Migration2605 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personnage_Parties_PartieId",
                table: "Personnage");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnage_Personnage_PersonnageId",
                table: "Personnage");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Personnage_JoueurId",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Personnage_JoueurEnAttaqueId",
                table: "Tours");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Personnage_JoueurEndefenseId",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Personnage_PartieId",
                table: "Personnage");

            migrationBuilder.DropIndex(
                name: "IX_Personnage_PersonnageId",
                table: "Personnage");

            migrationBuilder.DropColumn(
                name: "Etat",
                table: "Personnage");

            migrationBuilder.DropColumn(
                name: "OnAttack",
                table: "Personnage");

            migrationBuilder.DropColumn(
                name: "PartieId",
                table: "Personnage");

            migrationBuilder.DropColumn(
                name: "PersonnageId",
                table: "Personnage");

            migrationBuilder.DropColumn(
                name: "PointsExperiences",
                table: "Personnage");

            migrationBuilder.DropColumn(
                name: "TypeJoueur",
                table: "Personnage");

            migrationBuilder.DropColumn(
                name: "PersonnageJoueur_Degat",
                table: "Personnage");

            migrationBuilder.DropColumn(
                name: "PersonnageJoueur_PointsMagie",
                table: "Personnage");

            migrationBuilder.DropColumn(
                name: "PersonnageJoueur_Portee",
                table: "Personnage");

            migrationBuilder.CreateTable(
                name: "Joueurs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnageId = table.Column<int>(nullable: true),
                    PointsExperiences = table.Column<int>(nullable: true),
                    Etat = table.Column<int>(nullable: false),
                    OnAttack = table.Column<bool>(nullable: true),
                    PointsVie = table.Column<int>(nullable: true),
                    PointsMagie = table.Column<int>(nullable: true),
                    Portee = table.Column<int>(nullable: true),
                    Degat = table.Column<int>(nullable: true),
                    TypeJoueur = table.Column<int>(nullable: false),
                    PartieId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Joueurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Joueurs_Parties_PartieId",
                        column: x => x.PartieId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Joueurs_Personnage_PersonnageId",
                        column: x => x.PersonnageId,
                        principalTable: "Personnage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Joueurs_PartieId",
                table: "Joueurs",
                column: "PartieId");

            migrationBuilder.CreateIndex(
                name: "IX_Joueurs_PersonnageId",
                table: "Joueurs",
                column: "PersonnageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Joueurs_JoueurId",
                table: "Positions",
                column: "JoueurId",
                principalTable: "Joueurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Joueurs_JoueurEnAttaqueId",
                table: "Tours",
                column: "JoueurEnAttaqueId",
                principalTable: "Joueurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Joueurs_JoueurEndefenseId",
                table: "Tours",
                column: "JoueurEndefenseId",
                principalTable: "Joueurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Joueurs_JoueurId",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Joueurs_JoueurEnAttaqueId",
                table: "Tours");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Joueurs_JoueurEndefenseId",
                table: "Tours");

            migrationBuilder.DropTable(
                name: "Joueurs");

            migrationBuilder.AddColumn<int>(
                name: "Etat",
                table: "Personnage",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OnAttack",
                table: "Personnage",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartieId",
                table: "Personnage",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonnageId",
                table: "Personnage",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PointsExperiences",
                table: "Personnage",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeJoueur",
                table: "Personnage",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonnageJoueur_Degat",
                table: "Personnage",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonnageJoueur_PointsMagie",
                table: "Personnage",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonnageJoueur_Portee",
                table: "Personnage",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personnage_PartieId",
                table: "Personnage",
                column: "PartieId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnage_PersonnageId",
                table: "Personnage",
                column: "PersonnageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personnage_Parties_PartieId",
                table: "Personnage",
                column: "PartieId",
                principalTable: "Parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnage_Personnage_PersonnageId",
                table: "Personnage",
                column: "PersonnageId",
                principalTable: "Personnage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Personnage_JoueurId",
                table: "Positions",
                column: "JoueurId",
                principalTable: "Personnage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Personnage_JoueurEnAttaqueId",
                table: "Tours",
                column: "JoueurEnAttaqueId",
                principalTable: "Personnage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Personnage_JoueurEndefenseId",
                table: "Tours",
                column: "JoueurEndefenseId",
                principalTable: "Personnage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
