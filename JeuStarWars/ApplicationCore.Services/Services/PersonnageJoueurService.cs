


using ApplicationCore.Repository;
using ApplicationCore.services;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ApplicationCore.services
{
    public class PersonnageJoueurService : Repository<PersonnageJoueur, DataContext>,IPersonnageJoueurService
    {


        
        public PersonnageJoueurService(DataContext context) : base(context) 
        {
           
        }
       

        public IEnumerable<PersonnageJoueur> GetPersonnagesByCote(Cote cote)
        {
            #region A revoir (recuperer la du repository)

            IEnumerable<PersonnageJoueur> personnageJoueurs;
            if (cote == Cote.Lumineux)
                personnageJoueurs = FindAll().Where(p => p.Cote == Cote.Lumineux);
               
            else
                personnageJoueurs = FindAll().Where(p => p.Cote == Cote.Obscur);
               
            return personnageJoueurs;
        }

        #endregion
    }
}