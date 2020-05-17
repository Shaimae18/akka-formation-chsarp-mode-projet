using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.services
{
    public interface IPositionService
    {
        IEnumerable<Position> GetInitialPosition(Personnage currentPersonnage, int nombreEnnemie);
    }
}
