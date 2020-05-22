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
                new Position(1,6, new Joueur(){PointsVie=50,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant,Personnage= new PersonnageJoueur(pseudo:"Adv1",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
                new Position(3,4, new Joueur(){PointsVie=30,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant,Personnage= new PNJ(pseudo:"Adv2",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
                new Position(10,2, new Joueur(){PointsVie=100,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant,Personnage= new PNJ(pseudo:"The boss",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
                new Position(6,9, new Joueur(){PointsVie=10,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant,Personnage= new PNJ(pseudo:"Adv4",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
                new Position(7,2, new Joueur(){PointsVie=40,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant ,Personnage= new PNJ(pseudo:"Adv5",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
                new Position(9,3, new Joueur(){PointsVie=80,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant ,Personnage= new PNJ(pseudo:"Adv6",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
                new Position(6,1, new Joueur(){PointsVie=20,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant,Personnage= new PNJ(pseudo:"Adv7",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
                new Position(5,4, new Joueur(){PointsVie=20,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant,Personnage= new PNJ(pseudo:"Adv8",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
                new Position(1,1, new Joueur(){PointsVie=20,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant ,Personnage= new PNJ(pseudo:"Adv9",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
                new Position(1,9, new Joueur(){PointsVie=20,TypeJoueur= TypeJoueur.Adversaire,Etat=Etat.Vivant,Personnage= new PNJ(pseudo:"Adv10",cote:Cote.Obscur,typePersonnage:TypePersonnage.NonLanceurDeSort)}),
            };
        }
    }
}
