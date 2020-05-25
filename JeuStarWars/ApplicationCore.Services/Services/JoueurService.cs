using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Repository;
using Entities;


namespace ApplicationCore.services
{
   public class JoueurService : Repository<Joueur, DataContext>, IJoueurService
    {
        public JoueurService(DataContext context) : base(context) { }
    }
}
