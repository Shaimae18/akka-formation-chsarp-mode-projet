using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Partie
    {
       public List<Joueur> ListJoueurs { get; set; }
       public List<Tour> ListTours { get; set; }
        public Partie() 
        {
            ListJoueurs = new List<Joueur>();
            ListTours = new List<Tour>();
        }

    }
}
