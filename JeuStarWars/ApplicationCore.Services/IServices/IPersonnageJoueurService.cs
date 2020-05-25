using ApplicationCore.Repository;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.services
{
   public interface IPersonnageJoueurService : IRepository<PersonnageJoueur>
    {
        IEnumerable<PersonnageJoueur> GetPersonnagesByCote(Cote cote);

    }
}
