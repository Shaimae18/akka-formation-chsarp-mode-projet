using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.services
{
    public interface IAttaqueService
    {
        List<Position> GetChampsDattaques(Position currentJoueurPosition, int? portee, Grille borders);
        Position GetAdversaireAattaquer(IEnumerable<Position> listPosition, List<Position> listPosAuChampsDatt);
        bool JoueurIsInChampsAttaque(Position PositionJoueur, List<Position> listPosAuChampsDatt);
        bool Attaquer(Joueur JoueurAttaquant, Joueur JoueurAttaque);
    }
}
