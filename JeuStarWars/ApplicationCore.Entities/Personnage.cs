using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Personnage : BaseEntity
    {
        public TypePersonnage TypePersonnage { get; set; }
        public string Pseudo { get; set; }
        public int? PointsVie { get; set; }
        public Cote Cote { get; set; }
        public bool CanAttack { get; set; }
    }
    public enum Cote
    {
        Lumineux,
        Obscur
    }
}
