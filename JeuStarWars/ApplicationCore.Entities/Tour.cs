using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Tour : BaseEntity
    {
        public int NumeroDuTour { get; set; }
        public Joueur JoueurEnAttaque { get; set; }
        public Joueur JoueurEndefense { get; set; }
        public List<Position> ListPositionEnCours { get; set; }
        public string message { get; set; }
        public bool isMonTour { get; set; }
        public ActionTour ActionTour { get; set; }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (ActionTour == ActionTour.Deplacement)
                stringBuilder.Append($"Tour: {NumeroDuTour}  {JoueurEnAttaque.Personnage.Pseudo} {message} ");
            else
                stringBuilder.Append($"Tour: {NumeroDuTour}  En attaque: {JoueurEnAttaque.Personnage.Pseudo} Vs. En défense: {JoueurEndefense.Personnage.Pseudo} ");
            return stringBuilder.ToString();
        }

    }
    public enum ActionTour
        {
        Deplacement,
        Attaque
        }
}
