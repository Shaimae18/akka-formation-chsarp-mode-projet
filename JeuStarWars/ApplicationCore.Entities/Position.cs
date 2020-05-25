using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Position : BaseEntity
    {
        
        public int? X { get; set; }
        public int? Y { get; set; }
        public int LeftCursorPosition { get; set; }
        public int TopCursorPosition { get; set; }
        public Joueur Joueur { get; set; }

        public Position()
        {

        }
        public Position(int? x, int? y, Joueur joueur=null)
        {
            this.X = x;
            this.Y = y;
            this.Joueur= joueur ?? new Joueur();
        }
        public Position(int LeftCursorPosition, int topCursorPosition)
        {
            this.LeftCursorPosition = LeftCursorPosition;
            this.TopCursorPosition= topCursorPosition;
            
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
