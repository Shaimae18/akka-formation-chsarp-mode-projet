using ApplicationCore.Repository;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.services
{
    public class PartieService : Repository<Partie, DataContext>, IPartieService
    {
        private DataContext _context;
        public PartieService(DataContext context) : base(context) 
        {
            _context = context;
        }
        public override IEnumerable<Partie> FindAll()
        {
            return _context.Parties
                .Include(t => t.DernierTour)
                .ThenInclude(p => p.ListPositionEnCours);

        }
    }
}
