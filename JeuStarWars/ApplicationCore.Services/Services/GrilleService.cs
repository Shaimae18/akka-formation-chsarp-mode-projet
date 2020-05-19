using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.services
{
    public class GrilleService : IGrilleService
    {
        public GrilleService() : base() { }
        public IEnumerable<Position> GetInitialPositionInGrille(Joueur currentJoueur, int nombreEnnemie)
        {
            return new List<Position>()
            {
                new Position(2,9,currentJoueur),
                new Position(1,6, new Joueur(){PointsVie=1,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant,PersonnageJoueur= new PersonnageJoueur(pseudo:"Adv1",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
                new Position(3,4, new Joueur(){PointsVie=1,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant,PersonnageJoueur= new PersonnageJoueur(pseudo:"Adv1",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
                new Position(10,2, new Joueur(){PointsVie=1,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant,PersonnageJoueur= new PersonnageJoueur(pseudo:"Adv1",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
                new Position(6,9, new Joueur(){PointsVie=1,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant,PersonnageJoueur= new PersonnageJoueur(pseudo:"Adv1",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
                new Position(7,2, new Joueur(){PointsVie=1,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant ,PersonnageJoueur= new PersonnageJoueur(pseudo:"Adv1",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
                new Position(9,3, new Joueur(){PointsVie=1,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant ,PersonnageJoueur= new PersonnageJoueur(pseudo:"Adv1",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
                new Position(6,1, new Joueur(){PointsVie=1,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant,PersonnageJoueur= new PersonnageJoueur(pseudo:"Adv1",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
                new Position(5,4, new Joueur(){PointsVie=1,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant,PersonnageJoueur= new PersonnageJoueur(pseudo:"Adv1",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
                new Position(1,1, new Joueur(){PointsVie=1,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant ,PersonnageJoueur= new PersonnageJoueur(pseudo:"Adv1",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
                new Position(1,10, new Joueur(){PointsVie=1,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant,PersonnageJoueur= new PersonnageJoueur(pseudo:"Adv1",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
            };
        }
    }
}
