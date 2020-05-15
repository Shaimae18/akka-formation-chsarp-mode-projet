using System;

namespace Entities
{
    public class Personnage
    {
        public string Pseudo { get; set; }
        public Cote Cote { get; set; }
        public int? PointsVie { get; set; }
        public int? PointsMagie { get; set; }
        public int? Portee { get; set; }
        public int? Degat { get; set; }
        public Position Position { get; set; }


        public Personnage(string pseudo, Cote cote = Cote.Lumineux, int? pointsVie=150, int? pointsMagie=0, int? portee=0, int? degat=0, Position Position = null)
        {
            this.Pseudo = pseudo;
            this.Cote = cote;
            this.PointsVie = pointsVie;
            this.Portee = portee;
            this.Degat = degat;
            this.Position = new Position(Position?.X, Position?.Y);
        }


    }
    public enum Cote
    {
        Lumineux,
        Obscur
    }
}
