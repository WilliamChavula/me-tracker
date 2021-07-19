using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeTracker.Models;

namespace MeTracker.Repository
{
    public interface ILocationReposiroty
    {
        Task Save(Location location);
        Task<List<Location>> GetAll();
    }
}
