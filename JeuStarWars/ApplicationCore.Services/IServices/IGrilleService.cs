using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.services
{
    public interface IGrilleService
    {
        IEnumerable<Position> GetInitialPositionInGrille(Joueur currentJoueur, int nombreEnnemie);
    }
}
