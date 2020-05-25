using ApplicationCore.Repository;
using Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.services
{
    public class PartieService : Repository<Partie, DataContext>, IPartieService
    {
        public PartieService(DataContext context) : base(context) { }
    }
}
