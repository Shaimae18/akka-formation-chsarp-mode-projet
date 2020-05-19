

using Entities;
using System;
using System.Collections.Generic;
using System.Text;
namespace ApplicationCore.services
{
    public class PersonnageService : IPersonnageService
    {
        public PersonnageService() : base() { }

        public Personnage ChoisirPersonnages()
        {
            throw new NotImplementedException();
        }

        public bool DeplacerPersonnage(Personnage personnage, Position nouvellePos)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Personnage> GetAllPersonnages()
        {

            throw new NotImplementedException();
        }

        public IEnumerable<Personnage> GetPersonnagesByCote(Cote cote)
        {
            Personnage[] list;
            if (cote == Cote.Lumineux)
            {
                return list = new Personnage[4]
                {
                    new Personnage("Obiwan", pointsVie: 250, pointsMagie: 150, typePersonnage: TypePersonnage.Hero),
                    new Personnage("Yoda", pointsVie: 150, pointsMagie: 300, typePersonnage: TypePersonnage.Hero),
                    new Personnage("Mace", pointsVie: 250, pointsMagie: 250, typePersonnage: TypePersonnage.Hero),
                    new Personnage("Anakin", pointsVie: 200, pointsMagie: 200, typePersonnage: TypePersonnage.Hero)
                };
            }
            else
            {
                return list = new Personnage[4]
                {
                    new Personnage("Doku", pointsVie: 150, pointsMagie: 300, typePersonnage: TypePersonnage.Hero),
                    new Personnage("Grievious", pointsVie: 300, pointsMagie: 150, typePersonnage: TypePersonnage.Hero),
                    new Personnage("Empereur", pointsVie: 300, pointsMagie: 150, typePersonnage: TypePersonnage.Hero),
                    new Personnage("Jango", pointsVie: 350, pointsMagie: 100, typePersonnage: TypePersonnage.Hero)
                };
            }
        }   

        public bool SetPersonnagePosition(Position pos, Personnage personnage)
        {
            throw new NotImplementedException();
        }

    }
}