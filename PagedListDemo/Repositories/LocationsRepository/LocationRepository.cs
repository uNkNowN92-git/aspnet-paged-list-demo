using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedListDemo.Models;

namespace PagedListDemo.Repositories.LocationsRepository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly PagedListDemoEntities db = new PagedListDemoEntities();

        public IEnumerable<Location> GetAllLocations()
        {
            return db.Locations.ToList();
        }

        public void Insert(Location location)
        {
            db.Locations.Add(location);
            db.SaveChanges();
        }
    }
}