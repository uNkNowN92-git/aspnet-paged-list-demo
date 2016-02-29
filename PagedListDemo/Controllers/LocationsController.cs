using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http.Description;
using PagedListDemo.Models;
using System.Data.Entity.Spatial;
using PagedListDemo.Models.Locations;
using PagedListDemo.Common;
using PagedListDemo.Common.LocationUtils;
using System.Web.Mvc;
using PagedListDemo.Common.PagedList;

namespace PagedListDemo.Controllers
{
    public class LocationsController : Controller
    {
        private PagedListDemoEntities db = new PagedListDemoEntities();

        // GET: api/Locations
        [HttpGet]
        public JsonResult GetLocationsByDistance(LocationUtilsFilterOptions locationFilterOptions, ResultsFilter resultsFilter, PagedListOptions pagedListOptions)
        {
            var locationFilter = locationFilterOptions.ToLocationFilter();
            var minutes = resultsFilter.Minutes;
            pagedListOptions.SortBy = pagedListOptions.SortBy ?? "Distance";

            var locationsQuery = db.Locations
                .Where(x => x.Coordinate.Distance(locationFilter.Origin) <= locationFilter.Radius)
                .Select(x => new LocationModel
                {
                    LocationId = x.LocationID,
                    AddressLine1 = x.Address,
                    AddressLine2 = x.Address,
                    AddressLine3 = x.Address,
                    AddressLine4 = x.Address,
                    AddressLine5 = x.Address,
                    PostalCode = "BS234",
                    Distance = x.Coordinate.Distance(locationFilter.Origin)
                });
            
            var data = locationsQuery.ToPagedListData(pagedListOptions)
                .Select(x => new ResultModel
                {
                    LocationId = x.LocationId,
                    Address = x.Address,
                    Miles = x.Miles
                });

            var result = new PagedListResult<ResultModel>(data, locationsQuery.ToPagedListDetails());

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //// POST: api/Locations
        //public HttpStatusCodeResult PostLocation(JourneyModel newLocation)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var location = new Location
        //    {
        //        LocationID = newLocation.LocationId,
        //        Address = newLocation.Address,
        //        Coordinate = LocationUtilsHelpers.CreateDbGeography(newLocation.Longitude, newLocation.Latitude)
        //    };

        //    db.Locations.Add(location);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (LocationExists(location.LocationID))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = location.LocationID }, location);
        //}

        //// DELETE: api/Locations/5
        //[ResponseType(typeof(Location))]
        //public IHttpActionResult DeleteLocation(int id)
        //{
        //    Location location = db.Locations.Find(id);
        //    if (location == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Locations.Remove(location);
        //    db.SaveChanges();

        //    return Ok(location);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool LocationExists(int id)
        //{
        //    return db.Locations.Count(e => e.LocationID == id) > 0;
        //}
    }
}