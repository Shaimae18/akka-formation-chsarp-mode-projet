using ApplicationCore.Repository;
using Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.services
{
    public interface IPositionService : IRepository<Position>
    {
        IEnumerable<Position> GetInitialPosition(List<PNJ> enumerable);
    }
}
