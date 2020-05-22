using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class PNJ:Personnage
    {
        public PNJ(string pseudo, Cote cote = Cote.Obscur, int? pointsVie = 150,TypePersonnage typePersonnage = TypePersonnage.NonLanceurDeSort)
        {
          
            this.Pseudo = pseudo;
            this.Cote = Cote;
            this.PointsVie = pointsVie;
            this.TypePersonnage = typePersonnage;
       
        }
    }
}
