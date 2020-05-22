using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{

    public class Joueur :Personnage,IJoueur
    {
        public Personnage Personnage { get; set; }
        public int? PointsExperiences { get; set; }
        public Etat Etat { get; set; }
        public bool? OnAttack { get; set; }
        public int? PointsMagie { get; set ; }
        public int? Portee { get ; set; }
        public int? Degat { get ; set; }
        // a enlever du code
        public TypeJoueur TypeJoueur { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($" Joueur: {Personnage.Pseudo} PX: {PointsExperiences} PV: {PointsVie}  PM: {PointsMagie}   Portée: {Portee}   Dégats: {Degat} ");
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

