using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Position
    {
        
        public int? X { get; set; }
        public int? Y { get; set; }
        public int LeftCursorPosition { get; set; }
        public int TopCursorPosition { get; set; }
        public Personnage Personnage { get; set; }

       
        public Position(int? x, int? y, Personnage personnage=null)
        {
            this.X = x;
            this.Y = y;
            this.Personnage= personnage ?? new Personnage("");
        }
    }

 public enum TypeDeplacement
    {
        Up,
        Down,
        Left,
        Right
    }
}
