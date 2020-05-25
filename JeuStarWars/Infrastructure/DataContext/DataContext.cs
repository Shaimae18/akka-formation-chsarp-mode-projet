using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
   public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {


        }
        public DbSet<Partie> Parties { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Joueur> Joueurs { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PersonnageJoueur> PersonnageJoueurs { get; set; }
        public DbSet<PNJ> PNJs { get; set; }
        public DbSet<Parametrage> Parametrages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=PROBOOK-PC\SQLEXPRESS02;database=StarWars;trusted_connection=true;");
        }

    }

}

