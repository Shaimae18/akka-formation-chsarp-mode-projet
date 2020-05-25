using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Repository;
using Entities;


namespace ApplicationCore.services
{
    public class PositionService : Repository<Position, DataContext>, IPositionService
    {
        public PositionService(DataContext context) : base(context) 
        {
        
        }
     }
}
