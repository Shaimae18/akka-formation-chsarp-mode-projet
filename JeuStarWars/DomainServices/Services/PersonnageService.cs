using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class PersonnageService: IPersonnageService
    {
        public PersonnageService(): base() { }

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
            throw new NotImplementedException();
        }

        public bool SetPersonnagePosition(Position pos, Personnage personnage)
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
