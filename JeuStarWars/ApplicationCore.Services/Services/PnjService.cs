using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using ApplicationCore.Repository;

namespace ApplicationCore.services
{
    public class PNJService : Repository<PNJ, DataContext>, IPNJService
    {
        public PNJService(DataContext context) : base(context) { }
       
    }
}
