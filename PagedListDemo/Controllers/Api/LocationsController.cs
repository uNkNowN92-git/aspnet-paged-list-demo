using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PagedListDemo.Models;
using System.Data.Entity.Spatial;
using PagedListDemo.Models.Locations;

namespace PagedListDemo.Controllers.Api
{
    public class LocationsController : ApiController
    {
        private PagedListDemoEntities db = new PagedListDemoEntities();

        // GET: api/Locations
        //public IQueryable<LocationModel> GetLocations()
        //{
        //    //var locationModel = new LocationModel
        //    //{
        //    //    Distance = 300000,
        //    //    Longitude = 0,
        //    //    Latitude = 0
        //    //};
        //    //var origin = DbGeography.FromText(string.Format("POINT({0} {1})",
        //    //    locationModel.Longitude.GetValueOrDefault(), locationModel.Latitude.GetValueOrDefault()), 4326);

        //    //return db.Locations
        //    //    .Where(x => x.Coordinate.Distance(origin) < locationModel.Distance)
        //    //    .Select(x => new LocationModel
        //    //    {
        //    //        LocationId = x.LocationID,
        //    //        Address = x.Address,
        //    //        Distance = x.Coordinate.Distance(origin),
        //    //        Longitude = x.Coordinate.Longitude,
        //    //        Latitude = x.Coordinate.Latitude
        //    //    });
        //    var origin = DbGeography.FromText("POINT(0 0)", 4326);

        //    return db.Locations.Select(x => new LocationModel
        //    {
        //        LocationId = x.LocationID,
        //        Address = x.Address,
        //        Distance = x.Coordinate.Distance(origin),
        //        Longitude = x.Coordinate.Longitude,
        //        Latitude = x.Coordinate.Latitude
        //    });
        //}

        // GET: api/Locations
        public IEnumerable<LocationModel> GetLocationsByDistance([FromUri]LocationModel locationModel)
        {
            var origin = DbGeography.FromText(string.Format("POINT({0} {1})",
                locationModel.Longitude.GetValueOrDefault(), locationModel.Latitude.GetValueOrDefault()), 4326);

            //var locations = db.Locations
            //    .Where(x => x.Coordinate.Distance(origin) < locationModel.Distance)
            //    .Select(x => new LocationModel
            //    {
            //        LocationId = x.LocationID,
            //        Address = x.Address,
            //        Distance = x.Coordinate.Distance(origin),
            //        Longitude = x.Coordinate.Longitude,
            //        Latitude = x.Coordinate.Latitude
            //    }).ToList();

            locationModel.Distance = locationModel.Miles * 1600;

            var locations = db.Locations
                .Where(x => x.Coordinate.Distance(origin) < locationModel.Distance)
                .Select(x => new LocationModel
                {
                    LocationId = x.LocationID,
                    Address = x.Coordinate.Difference(origin).Distance(origin).ToString(),
                    Distance = x.Coordinate.Distance(origin),
                    Longitude = x.Coordinate.Longitude,
                    Latitude = x.Coordinate.Latitude
                }).ToList();

            locations.ForEach(location =>
            {
                location.Miles = GetDistance(
                    origin.Latitude.GetValueOrDefault(),
                    origin.Longitude.GetValueOrDefault(), 
                    location.Latitude.GetValueOrDefault(),
                    location.Longitude.GetValueOrDefault());

                location.Ratio = location.Distance / location.Miles;
            });

            return locations;
        }

        public static double GetDistance(double lat1, double lon1, double lat2, double lon2, char unit = 'M')
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }

            return (dist);
        }

        public static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rad"></param>
        /// <returns></returns>
        public static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        // GET: api/Locations/5
        //[ResponseType(typeof(Location))]
        //public IHttpActionResult GetLocation(int id)
        //{
        //    Location location = db.Locations.Find(id);
        //    if (location == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(location);
        //}

        // PUT: api/Locations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLocation(int id, Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.LocationID)
            {
                return BadRequest();
            }

            db.Entry(location).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Locations
        [ResponseType(typeof(Location))]
        public IHttpActionResult PostLocation(LocationModel locationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = new Location
            {
                LocationID = locationModel.LocationId,
                Address = locationModel.Address,
                Coordinate = DbGeography.FromText(string.Format("POINT({0} {1})", locationModel.Longitude, locationModel.Latitude), 4326)
            };

            db.Locations.Add(location);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LocationExists(location.LocationID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = location.LocationID }, location);
        }

        // DELETE: api/Locations/5
        [ResponseType(typeof(Location))]
        public IHttpActionResult DeleteLocation(int id)
        {
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            db.Locations.Remove(location);
            db.SaveChanges();

            return Ok(location);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocationExists(int id)
        {
            return db.Locations.Count(e => e.LocationID == id) > 0;
        }
    }
}