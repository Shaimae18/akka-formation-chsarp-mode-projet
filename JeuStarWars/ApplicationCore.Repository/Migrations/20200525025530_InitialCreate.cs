using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationCore.Repository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreation = table.Column<DateTime>(nullable: false),
                    DateDernierSauvgarde = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parametrages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomParametre = table.Column<string>(nullable: true),
                    Valeur = table.Column<string>(nullable: true),
                    PartieId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametrages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parametrages_Parties_PartieId",
                        column: x => x.PartieId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personnage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypePersonnage = table.Column<int>(nullable: false),
                    Pseudo = table.Column<string>(nullable: true),
                    PointsVie = table.Column<int>(nullable: true),
                    Cote = table.Column<int>(nullable: false),
                    CanAttack = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    PersonnageId = table.Column<int>(nullable: true),
                    PointsExperiences = table.Column<int>(nullable: true),
                    Etat = table.Column<int>(nullable: true),
                    OnAttack = table.Column<bool>(nullable: true),
                    PointsMagie = table.Column<int>(nullable: true),
                    Portee = table.Column<int>(nullable: true),
                    Degat = table.Column<int>(nullable: true),
                    TypeJoueur = table.Column<int>(nullable: true),
                    PartieId = table.Column<int>(nullable: true),
                    PersonnageJoueur_PointsMagie = table.Column<int>(nullable: true),
                    PersonnageJoueur_Portee = table.Column<int>(nullable: true),
                    PersonnageJoueur_Degat = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personnage_Parties_PartieId",
                        column: x => x.PartieId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personnage_Personnage_PersonnageId",
                        column: x => x.PersonnageId,
                        principalTable: "Personnage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<int>(nullable: true),
                    Y = table.Column<int>(nullable: true),
                    LeftCursorPosition = table.Column<int>(nullable: false),
                    TopCursorPosition = table.Column<int>(nullable: false),
                    JoueurId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Personnage_JoueurId",
                        column: x => x.JoueurId,
                        principalTable: "Personnage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroDuTour = table.Column<int>(nullable: false),
                    JoueurEnAttaqueId = table.Column<int>(nullable: true),
                    JoueurEndefenseId = table.Column<int>(nullable: true),
                    message = table.Column<string>(nullable: true),
                    ActionTour = table.Column<int>(nullable: false),
                    PartieId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tours_Personnage_JoueurEnAttaqueId",
                        column: x => x.JoueurEnAttaqueId,
                        principalTable: "Personnage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tours_Personnage_JoueurEndefenseId",
                        column: x => x.JoueurEndefenseId,
                        principalTable: "Personnage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tours_Parties_PartieId",
                        column: x => x.PartieId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parametrages_PartieId",
                table: "Parametrages",
                column: "PartieId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnage_PartieId",
                table: "Personnage",
                column: "PartieId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnage_PersonnageId",
                table: "Personnage",
                column: "PersonnageId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_JoueurId",
                table: "Positions",
                column: "JoueurId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_JoueurEnAttaqueId",
                table: "Tours",
                column: "JoueurEnAttaqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_JoueurEndefenseId",
                table: "Tours",
                column: "JoueurEndefenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_PartieId",
                table: "Tours",
                column: "PartieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parametrages");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Personnage");

            migrationBuilder.DropTable(
                name: "Parties");
        }
    }
}
