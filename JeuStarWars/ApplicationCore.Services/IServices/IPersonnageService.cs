using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.services
{
    public interface IPersonnageService
    {
        IEnumerable<PersonnageJoueur> GetAllPersonnages();
        IEnumerable<PersonnageJoueur> GetPersonnagesByCote(Cote cote);
       

    }
}
