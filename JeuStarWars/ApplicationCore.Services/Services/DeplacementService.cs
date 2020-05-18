﻿using Entities;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.services
{
    public class DeplacementService: IDeplacementService
    {
        public DeplacementService() : base() 
        {
        }

        

        public IEnumerable<Position> GetInitialPosition(Personnage currentPersonnage, int nombreEnnemie)
        {
            return new List<Position>()
            {
                new Position(2,9,currentPersonnage),
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
        public bool CheckMoveValidity(TypeDeplacement typeDeplacement, Position currentPos, Grille grille)
        {
            switch (typeDeplacement)
            {
                case TypeDeplacement.Down:
                    return currentPos.TopCursorPosition +3 <  (grille.BottomBorder);
                case TypeDeplacement.Up:
                    return currentPos.TopCursorPosition-3 > (grille.TopBorder);
                case TypeDeplacement.Left:
                    return currentPos.LeftCursorPosition-5 > (grille.LeftBorder);
                case TypeDeplacement.Right:
                    return currentPos.LeftCursorPosition+5 < (grille.RightBorder);
                default:
                    return false;
            }
        }

        public bool CheckIfCaseIsEmpty(TypeDeplacement typeDeplacement, Position position, IEnumerable<Position> listPosition)
        {
            switch (typeDeplacement)
            {
                case TypeDeplacement.Down:
                    return !listPosition.Where( pos => pos.LeftCursorPosition== position.LeftCursorPosition  && pos.TopCursorPosition == position.TopCursorPosition+3).Any();
                case TypeDeplacement.Up:
                    return !listPosition.Where(pos => pos.LeftCursorPosition == position.LeftCursorPosition  && pos.TopCursorPosition == position.TopCursorPosition-3).Any();
                case TypeDeplacement.Left:
                    return !listPosition.Where(pos => pos.LeftCursorPosition == position.LeftCursorPosition -5 && pos.TopCursorPosition == position.TopCursorPosition).Any();
                case TypeDeplacement.Right:
                    return !listPosition.Where(pos => pos.LeftCursorPosition == position.LeftCursorPosition +5 && pos.TopCursorPosition == position.TopCursorPosition).Any();
                default:
                    return false;
            }
        }

        public Dictionary<TypeDeplacement, Position> DeplacerLePlusProcheEnnemie(Position currentPosition, IEnumerable<Position> listPosition, Grille grille)
        {
            Dictionary<TypeDeplacement, Position> dictDeplacement = new Dictionary<TypeDeplacement, Position>();
            Dictionary<TypeDeplacement, double> dictDist = new Dictionary<TypeDeplacement, double>();
            Position pos = GetLePlusProcheEnnemie(currentPosition, listPosition.Where(p => p.Personnage.TypePersonnage == TypePersonnage.Ennemie));
            Position positionAfterDep = new Position(null, null);

            #region Up
            if (CheckMoveValidity(TypeDeplacement.Up, pos, grille)
                && CheckIfCaseIsEmpty(TypeDeplacement.Up, pos, listPosition))
            {
                positionAfterDep.LeftCursorPosition = pos.LeftCursorPosition;
                positionAfterDep.TopCursorPosition = pos.TopCursorPosition - 3;
                dictDist.Add(TypeDeplacement.Up, GetDistanceBetweenPos(currentPosition, positionAfterDep));
            }

            #endregion
            #region Down
            if (CheckMoveValidity(TypeDeplacement.Down, pos, grille)
                && CheckIfCaseIsEmpty(TypeDeplacement.Down, pos, listPosition))
            {
                positionAfterDep.LeftCursorPosition = pos.LeftCursorPosition;
                positionAfterDep.TopCursorPosition = pos.TopCursorPosition + 3;
                dictDist.Add(TypeDeplacement.Down, GetDistanceBetweenPos(currentPosition, positionAfterDep));
            }
            #endregion
            #region Left
            if (CheckMoveValidity(TypeDeplacement.Left, pos, grille)
                && CheckIfCaseIsEmpty(TypeDeplacement.Left, pos, listPosition))
            {
                positionAfterDep.LeftCursorPosition = pos.LeftCursorPosition - 5;
                positionAfterDep.TopCursorPosition = pos.TopCursorPosition;
                dictDist.Add(TypeDeplacement.Left, GetDistanceBetweenPos(currentPosition, positionAfterDep));
            }
            #endregion
            #region Right
            if (CheckMoveValidity(TypeDeplacement.Right, pos, grille)
                && CheckIfCaseIsEmpty(TypeDeplacement.Right, pos, listPosition))
            {
                positionAfterDep.LeftCursorPosition = pos.LeftCursorPosition + 5;
                positionAfterDep.TopCursorPosition = pos.TopCursorPosition;
                dictDist.Add(TypeDeplacement.Right, GetDistanceBetweenPos(currentPosition, positionAfterDep));
            }
            #endregion
            var typeDepConvenable = dictDist.MinBy(x => x.Value).First().Key;
            dictDeplacement.Add(typeDepConvenable,pos);
            return dictDeplacement;
        }

        private Position GetLePlusProcheEnnemie(Position currentPosition, IEnumerable<Position> listPositionEnnemie)
        {
            Position posPlusProche = null;
            double minDistance = Int32.MaxValue;
            foreach(Position posEnnemie in listPositionEnnemie)
            {
                double distance = GetDistanceBetweenPos(currentPosition,posEnnemie);
                minDistance = Math.Min(distance,minDistance);
                if (minDistance == distance)
                    posPlusProche = posEnnemie;
            }
            return posPlusProche;
        }
        double GetDistanceBetweenPos(Position pos1 , Position pos2)
        {
            return Math.Sqrt(Math.Pow((pos1.LeftCursorPosition - pos2.LeftCursorPosition), 2) + Math.Pow((pos1.TopCursorPosition - pos2.TopCursorPosition), 2));
        }
    }
}
