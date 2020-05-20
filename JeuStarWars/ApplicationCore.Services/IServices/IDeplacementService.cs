using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.services
{
    public interface IDeplacementService
    {
      
        bool CheckMoveValidity(TypeDeplacement typeDeplacement,Position currentPos, Grille border);
        bool CheckIfCaseIsEmpty(TypeDeplacement typeDeplacement, Position position, IEnumerable<Position> listPosition);
        Dictionary<TypeDeplacement, Position> Deplacer(Position currentPosition, Position posPlusProcheEnnemie, IEnumerable<Position> listPosition, Grille grille);
        Position GetLePlusProcheEnnemie(Position currentPosition,  IEnumerable<Position> listPositionEnnemie);
        List<Dictionary<TypeDeplacement, Position>> DeplacerTousEnnemie(Position currentPositionJoueur, IEnumerable<Position> listPosition, Grille grille);
    }
}
