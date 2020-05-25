using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Repository;
using Entities;


namespace ApplicationCore.services
{
    public class TourService : Repository<Tour, DataContext>, ITourService
    {
        public TourService(DataContext context) : base(context)
        {

        }
    }
}
