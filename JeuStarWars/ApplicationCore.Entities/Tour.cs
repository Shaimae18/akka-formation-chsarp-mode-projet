using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Tour
    {
        public int numeroDuTour;
        public Joueur JoueurEnAttaque { get; set; }
        public Joueur JoueurEndefense { get; set; }
        public string message { get; set; }
        public ActionTour ActionTour { get; set; }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (ActionTour == ActionTour.Deplacement)
                stringBuilder.Append($"Tour: {numeroDuTour}  {JoueurEnAttaque.PersonnageJoueur.Pseudo} {message} ");
            else
                stringBuilder.Append($"Tour: {numeroDuTour}  En attaque: {JoueurEnAttaque.PersonnageJoueur.Pseudo} Vs. En défense: {JoueurEndefense.PersonnageJoueur.Pseudo} ");
            return stringBuilder.ToString();
        }

    }
    public enum ActionTour
        {
        Deplacement,
        Attaque
        }
}
