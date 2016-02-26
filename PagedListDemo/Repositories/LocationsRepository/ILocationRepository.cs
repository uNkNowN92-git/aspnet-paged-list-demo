using PagedListDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagedListDemo.Repositories.LocationsRepository
{
    [ServiceInterface]
    public interface ILocationRepository
    {
        IEnumerable<Location> GetAllLocations();

        void Insert(Location location);
    }
}
