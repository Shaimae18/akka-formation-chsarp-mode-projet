using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.services
{
    public interface IPersonnageService
    {
        IEnumerable<Personnage> GetAllPersonnages();
        IEnumerable<Personnage> GetPersonnagesByCote(Cote cote);
        Personnage ChoisirPersonnages();
        bool DeplacerPersonnage(Personnage personnage, Position nouvellePos);
        bool SetPersonnagePosition(Position pos, Personnage personnage);
        

    }
}
