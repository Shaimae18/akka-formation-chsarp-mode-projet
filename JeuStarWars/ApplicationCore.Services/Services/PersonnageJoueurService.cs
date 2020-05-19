

using Entities;
using System;
using System.Collections.Generic;
using System.Text;
namespace ApplicationCore.services
{
    public class PersonnageJoueurService : IPersonnageService
    {
        public PersonnageJoueurService() : base() { }

        public IEnumerable<PersonnageJoueur> GetAllPersonnages()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonnageJoueur> GetPersonnagesByCote(Cote cote)
        {
            #region A revoir (recuperer la du repository)
            PersonnageJoueur[] list;
            if (cote == Cote.Lumineux)
            {
                return list = new PersonnageJoueur[5]
                {
                    new PersonnageJoueur("Obiwan",cote:Cote.Lumineux, pointsVie: 250, pointsMagie: 150, typePersonnage: TypePersonnage.LanceurDeSort),
                    new PersonnageJoueur("Yoda",cote:Cote.Lumineux, pointsVie: 150, pointsMagie: 300, typePersonnage: TypePersonnage.LanceurDeSort),
                    new PersonnageJoueur("Mace",cote:Cote.Lumineux, pointsVie: 250, pointsMagie: 250, typePersonnage: TypePersonnage.LanceurDeSort),
                    new PersonnageJoueur("Anakin",cote:Cote.Lumineux, pointsVie: 200, pointsMagie: 200, typePersonnage: TypePersonnage.LanceurDeSort),
                    new PersonnageJoueur("Vala", cote:Cote.Lumineux,pointsVie: 200, pointsMagie: 0, typePersonnage: TypePersonnage.NonLanceurDeSort)
                };
            }
            else
            {
                return list = new PersonnageJoueur[5]
                {
                    new PersonnageJoueur("Doku",cote:Cote.Obscur, pointsVie: 150, pointsMagie: 300, typePersonnage: TypePersonnage.LanceurDeSort),
                    new PersonnageJoueur("Grievious", cote:Cote.Obscur,pointsVie: 300, pointsMagie: 150, typePersonnage: TypePersonnage.LanceurDeSort),
                    new PersonnageJoueur("Empereur", cote:Cote.Obscur,pointsVie: 300, pointsMagie: 150, typePersonnage: TypePersonnage.LanceurDeSort),
                    new PersonnageJoueur("Jango",cote:Cote.Obscur, pointsVie: 350, pointsMagie: 100, typePersonnage: TypePersonnage.LanceurDeSort),
                    new PersonnageJoueur("Ventress",cote:Cote.Obscur, pointsVie: 350, pointsMagie: 0, typePersonnage: TypePersonnage.NonLanceurDeSort)
                };
            }
        }

        #endregion
    }
}