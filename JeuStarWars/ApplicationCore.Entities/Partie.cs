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
        public Tour DernierTour { get; set; }

         public Partie() 
         {
            DateCreation = DateTime.Today;
           
         }

    }
}
