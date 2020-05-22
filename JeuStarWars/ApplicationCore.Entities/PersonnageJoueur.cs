using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Entities
{
    public class PersonnageJoueur : Personnage, IJoueur
    {
        public int? PointsMagie { get ; set ; }
        public int? Portee { get; set; }
        public int? Degat {get; set ; }
        public PersonnageJoueur(string pseudo, Cote cote = Cote.Lumineux, int? pointsVie = 150, int? pointsMagie = 0, int? portee = 0, int? degat = 0, TypePersonnage typePersonnage= TypePersonnage.NonLanceurDeSort)
        {
            this.Pseudo = pseudo;
            this.Cote = cote;
            this.PointsVie = pointsVie;
            this.PointsMagie = pointsMagie;
            this.Portee = portee;
            this.Degat = degat;
            this.TypePersonnage = typePersonnage;
            this.CanAttack = true;
        }

        public PersonnageJoueur(PersonnageJoueur personnage)
        {
            this.Pseudo = personnage.Pseudo;
            this.Cote = personnage.Cote;
            this.PointsVie = personnage.PointsVie;
            this.PointsMagie = personnage.PointsMagie;
            this.Portee = personnage.Portee;
            this.Degat = personnage.Degat;
            this.TypePersonnage = personnage.TypePersonnage;
            this.CanAttack = true;
        }

        public PersonnageJoueur()
        {
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
         
            stringBuilder.Append($" Joueur: {Pseudo}  PV: {PointsVie}  PM: {PointsMagie}   Portée: {Portee}   Dégats: {Degat} ");
            return stringBuilder.ToString();
        }
    }
    public enum TypePersonnage
    {
        LanceurDeSort,
        NonLanceurDeSort
    }
}
