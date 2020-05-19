using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Joueur
    {
        public PersonnageJoueur PersonnageJoueur { get; set; }
        public TypeJoueur TypeJoueur { get; set; }
        public int? PointsExperiences { get; set; }
        public int? PointsVie { get; set; }
        public int? PointsMagie { get; set; }
        public int? Portee { get; set; }
        public int? Degat { get; set; }
        public Etat Etat { get; set; }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($" Joueur: {PersonnageJoueur.Pseudo} PX: {PointsExperiences} PV: {PointsVie}  PM: {PointsMagie}   Portée: {Portee}   Dégats: {Degat} ");
            return stringBuilder.ToString();
        }
    }
    public enum TypeJoueur
    {
        Joueur,
        Adversaire
    }
    public enum Etat
    {
        Mort,
        Vivant
    }

}

