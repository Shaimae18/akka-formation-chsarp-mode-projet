using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.services
{
   public class AttaqueService: IAttaqueService
    {
        public AttaqueService():base()
        {
            
        }

       

        public List<Position> GetChampsDattaques(Position currentJoueurPosition,int? portee,Grille borders)
        {
            List<Position> ListPosChampsAttq = new List<Position>();
            Position pos;

            pos = new Position(currentJoueurPosition.LeftCursorPosition, currentJoueurPosition.TopCursorPosition-3) ;
            if (CheckValidity(pos, borders))
                ListPosChampsAttq.Add(pos);

            pos = new Position(currentJoueurPosition.LeftCursorPosition, currentJoueurPosition.TopCursorPosition +3);
            if (CheckValidity(pos, borders))
                ListPosChampsAttq.Add(pos);

            pos = new Position(currentJoueurPosition.LeftCursorPosition-5, currentJoueurPosition.TopCursorPosition - 3);
            if (CheckValidity(pos, borders))
                ListPosChampsAttq.Add(pos);

            pos = new Position(currentJoueurPosition.LeftCursorPosition+5, currentJoueurPosition.TopCursorPosition - 3);
            if (CheckValidity(pos, borders))
                ListPosChampsAttq.Add(pos);

            pos = new Position(currentJoueurPosition.LeftCursorPosition+5, currentJoueurPosition.TopCursorPosition + 3);
            if (CheckValidity(pos, borders))
                ListPosChampsAttq.Add(pos);

            pos = new Position(currentJoueurPosition.LeftCursorPosition-5, currentJoueurPosition.TopCursorPosition +3);
            if (CheckValidity(pos, borders))
                ListPosChampsAttq.Add(pos);

            pos = new Position(currentJoueurPosition.LeftCursorPosition-5, currentJoueurPosition.TopCursorPosition);
            if (CheckValidity(pos, borders))
                ListPosChampsAttq.Add(pos);

            pos = new Position(currentJoueurPosition.LeftCursorPosition+5, currentJoueurPosition.TopCursorPosition);
                ListPosChampsAttq.Add(pos);
            return ListPosChampsAttq;
        }
        public Position GetAdversaireAattaquer(IEnumerable<Position> listPosition, List<Position> listPosAuChampsDatt)
        {
            return listPosition.Where(p => listPosAuChampsDatt.FirstOrDefault(pos => pos.LeftCursorPosition == p.LeftCursorPosition && pos.TopCursorPosition == p.TopCursorPosition && p.Joueur.Etat!= Etat.Mort)!=null).FirstOrDefault();
        }

        private bool CheckValidity(Position pos, Grille borders)
        {
            return pos.LeftCursorPosition.Between((int)borders.LeftBorder, (int)borders.RightBorder)
                 && pos.TopCursorPosition.Between((int)borders.TopBorder, (int)borders.BottomBorder);
        }

        public bool Attaquer(Joueur joueurAttaquant, Joueur joueurAttaque)
        {
            bool isDead = true;
            if (joueurAttaque.PersonnageJoueur.TypePersonnage == TypePersonnage.NonLanceurDeSort)
                joueurAttaquant.PointsVie+= 1;
            else
                joueurAttaquant.PointsVie =joueurAttaquant.PointsVie + 10;

            joueurAttaque.PointsVie -= 1;
            if (joueurAttaque.PointsVie <= 0)
                joueurAttaque.Etat = Etat.Mort;
            else
                isDead = false;
            return isDead;
        }
    }
}
