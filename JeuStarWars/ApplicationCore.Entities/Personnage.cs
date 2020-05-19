using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Personnage
    {
        public string Pseudo { get; set; }
        public Cote Cote { get; set; }
    }
    public enum Cote
    {
        Lumineux,
        Obscur
    }
}
