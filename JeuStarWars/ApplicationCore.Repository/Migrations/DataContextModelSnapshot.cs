﻿// <auto-generated />
using System;
using ApplicationCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApplicationCore.Repository.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Joueur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Degat")
                        .HasColumnType("int");

                    b.Property<int>("Etat")
                        .HasColumnType("int");

                    b.Property<bool?>("OnAttack")
                        .HasColumnType("bit");

                    b.Property<int?>("PersonnageId")
                        .HasColumnType("int");

                    b.Property<int?>("PointsExperiences")
                        .HasColumnType("int");

                    b.Property<int?>("PointsMagie")
                        .HasColumnType("int");

                    b.Property<int?>("PointsVie")
                        .HasColumnType("int");

                    b.Property<int?>("Portee")
                        .HasColumnType("int");

                    b.Property<int>("TypeJoueur")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonnageId");

                    b.ToTable("Joueurs");
                });

            modelBuilder.Entity("Entities.Parametrage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomParametre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Valeur")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Parametrages");
                });

            modelBuilder.Entity("Entities.Partie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateDernierSauvgarde")
                        .HasColumnType("datetime2");

                    b.Property<int>("Resultat")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("Entities.Personnage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CanAttack")
                        .HasColumnType("bit");

                    b.Property<int>("Cote")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PointsVie")
                        .HasColumnType("int");

                    b.Property<string>("Pseudo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypePersonnage")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Personnage");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Personnage");
                });

            modelBuilder.Entity("Entities.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("JoueurId")
                        .HasColumnType("int");

                    b.Property<int>("LeftCursorPosition")
                        .HasColumnType("int");

                    b.Property<int>("TopCursorPosition")
                        .HasColumnType("int");

                    b.Property<int?>("TourId")
                        .HasColumnType("int");

                    b.Property<int?>("X")
                        .HasColumnType("int");

                    b.Property<int?>("Y")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("JoueurId");

                    b.HasIndex("TourId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Entities.Tour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActionTour")
                        .HasColumnType("int");

                    b.Property<int?>("JoueurEnAttaqueId")
                        .HasColumnType("int");

                    b.Property<int?>("JoueurEndefenseId")
                        .HasColumnType("int");

                    b.Property<int>("NumeroDuTour")
                        .HasColumnType("int");

                    b.Property<int?>("PartieId")
                        .HasColumnType("int");

                    b.Property<bool>("isMonTour")
                        .HasColumnType("bit");

                    b.Property<string>("message")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("JoueurEnAttaqueId");

                    b.HasIndex("JoueurEndefenseId");

                    b.HasIndex("PartieId");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("Entities.PNJ", b =>
                {
                    b.HasBaseType("Entities.Personnage");

                    b.HasDiscriminator().HasValue("PNJ");
                });

            modelBuilder.Entity("Entities.PersonnageJoueur", b =>
                {
                    b.HasBaseType("Entities.Personnage");

                    b.Property<int?>("Degat")
                        .HasColumnType("int");

                    b.Property<int?>("PointsMagie")
                        .HasColumnType("int");

                    b.Property<int?>("Portee")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("PersonnageJoueur");
                });

            modelBuilder.Entity("Entities.Joueur", b =>
                {
                    b.HasOne("Entities.Personnage", "Personnage")
                        .WithMany()
                        .HasForeignKey("PersonnageId");
                });

            modelBuilder.Entity("Entities.Position", b =>
                {
                    b.HasOne("Entities.Joueur", "Joueur")
                        .WithMany()
                        .HasForeignKey("JoueurId");

                    b.HasOne("Entities.Tour", null)
                        .WithMany("ListPositionEnCours")
                        .HasForeignKey("TourId");
                });

            modelBuilder.Entity("Entities.Tour", b =>
                {
                    b.HasOne("Entities.Joueur", "JoueurEnAttaque")
                        .WithMany()
                        .HasForeignKey("JoueurEnAttaqueId");

                    b.HasOne("Entities.Joueur", "JoueurEndefense")
                        .WithMany()
                        .HasForeignKey("JoueurEndefenseId");

                    b.HasOne("Entities.Partie", null)
                        .WithMany("ListTours")
                        .HasForeignKey("PartieId");
                });
#pragma warning restore 612, 618
        }
    }
}
