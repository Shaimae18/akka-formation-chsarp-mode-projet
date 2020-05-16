using System;
using System.Text;

namespace Entities
{
    public class Personnage
    {
        public string Pseudo { get; set; }
        public Cote Cote { get; set; }
        public TypePersonnage TypePersonnage { get; set; }
        public int? PointsVie { get; set; }
        public int? PointsMagie { get; set; }
        public int? Portee { get; set; }
        public int? Degat { get; set; }
        public Position Position { get; set; }


        public Personnage(string pseudo, Cote cote = Cote.Lumineux,TypePersonnage typePersonnage = TypePersonnage.Ennemie, int? pointsVie=150, int? pointsMagie=0, int? portee=0, int? degat=0, Position Position = null)
        {
            this.Pseudo = pseudo;
            this.Cote = cote;
            this.TypePersonnage = typePersonnage;
            this.PointsVie = pointsVie;
            this.PointsMagie = pointsMagie;
            this.Portee = portee;
            this.Degat = degat;
            
        }
        public override string ToString()
        {
            //stringBuilder.Append("╔═══════════════════╗╔═══════════════════╗╔═══════════════════╗╔═══════════════════╗╔═══════════════════╗╔═══════════════════╗");
            //stringBuilder.Append("║      Pseudo       ║║        Côté       ║║    Points de vie  ║║   Points de Magie ║║       Portée      ║║        Degat      ║");
            StringBuilder stringBuilder = new StringBuilder();
           
            stringBuilder.Append($" Joueur: {Pseudo}  PV: {PointsVie}  PM: {PointsMagie}   Portée: {Portee}   Dégats: {Degat} ");
         
        

            return stringBuilder.ToString() ;
        }

    }
    public enum Cote
    {
        Lumineux,
        Obscur
    }
    public enum TypePersonnage
    {
        Hero,
        SpecialHero,
        Ennemie
    }
}
