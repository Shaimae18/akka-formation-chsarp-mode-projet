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
        Dictionary<TypeDeplacement, Position> DeplacerLePlusProcheEnnemie(Position currentPosition, IEnumerable<Position> listPosition, Grille grille);
    }
}
