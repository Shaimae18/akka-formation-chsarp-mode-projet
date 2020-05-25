using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using ApplicationCore.Repository;

namespace ApplicationCore.services
{
    public class ParametrageService : Repository<Parametrage, DataContext>, IParametrageService
    {
        public ParametrageService(DataContext context) : base(context) { }
    }
}
