using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using ApplicationCore.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApplicationCore.services
{
    public class ParametrageService : Repository<Parametrage, DataContext>, IParametrageService
    {
        private DataContext _context;
        private DbSet<Parametrage> _dbSet { get; set; }
        public ParametrageService(DataContext context) : base(context) 
        {
            _context = context;
            _dbSet = _context.Set<Parametrage>();

        }
        public override int Insert(Parametrage entity)
        {
            Parametrage parametrage = _context.Parametrages.Where(p => p.NomParametre == entity.NomParametre).FirstOrDefault();
            if (parametrage != null)
                Delete(parametrage);
            return base.Insert(entity);
        }

        public void ReintialiserParametrage()
        {
           
           
        }
    }
}
