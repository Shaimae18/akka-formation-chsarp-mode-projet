using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.services
{
    public class PositionService: IPositionService
    {
        public PositionService() : base() 
        {
        }

        public IEnumerable<Position> GetInitialPosition(Personnage currentPersonnage, int nombreEnnemie)
        {
            return new List<Position>()
            {
                new Position(2,5,currentPersonnage),
                new Position(1,6),
                new Position(3,4),
                new Position(10,2),
                new Position(6,9),
                new Position(7,2),
                new Position(9,3),
                new Position(6,1),
                new Position(5,4),
                new Position(1,1),
                new Position(0,10),
            };
        }
    }
}
