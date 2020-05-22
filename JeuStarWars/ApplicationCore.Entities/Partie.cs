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
        public List<Joueur> ListJoueurs { get; set; }
       public List<Tour> ListTours { get; set; }
        public Partie() 
        {
            DateCreation = DateTime.Today;
            ListJoueurs = new List<Joueur>();
            ListTours = new List<Tour>();
        }

    }
}
