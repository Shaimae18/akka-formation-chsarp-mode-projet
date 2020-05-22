using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Parametrage:BaseEntity
    {
        public string NomParametre { get; set; }
        public string Valeur { get; set; }
        public Partie Partie { get; set; }
    }
}
