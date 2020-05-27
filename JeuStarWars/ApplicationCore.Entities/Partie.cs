using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Partie : BaseEntity
    {
        string NomPartie { get; set; }

        public DateTime DateCreation { get; set; }
        public DateTime DateDernierSauvgarde { get; set; }
        public Resultat Resultat { get; set; }
        public List<Tour> ListTours { get; set; }


         public Partie() 
         {
            DateCreation = DateTime.Today;
            ListTours = new List<Tour>();
         }

    }
    public enum Resultat
    {
        Gagne,
        Perdu
    }
}
