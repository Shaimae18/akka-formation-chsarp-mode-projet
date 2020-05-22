using Entities;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.Repository
{
    class PartieRepository : Repository<Partie, DataContext>, IPartieRepository
    {
        public PartieRepository(DataContext context) : base(context) { }
    }
}
