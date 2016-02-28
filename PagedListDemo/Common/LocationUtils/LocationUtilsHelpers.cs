using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace PagedListDemo.Common.LocationUtils
{
    public static class LocationUtilsHelpers
    {
        /// <summary>
        /// Gets the distance in miles of the given coordinates.
        /// </summary>
        /// <param name="longitudeA"></param>
        /// <param name="latitudeA"></param>
        /// <param name="longitudeB"></param>
        /// <param name="latitudeB"></param>
        /// <param name="fractionalDigits"></param>
        /// <returns></returns>
        public static double GetDistance(double longitudeA, double latitudeA, double longitudeB, double latitudeB, int? fractionalDigits = null)
        {
            var start = CreateDbGeographyPoint(longitudeA, latitudeA);
            var end = CreateDbGeographyPoint(longitudeB, latitudeB);
            var distance = start.Distance(end).GetValueOrDefault() / LocationUtilsConstants.MilesToMeters;

            if (fractionalDigits.HasValue && fractionalDigits >= 0)
            {
                return Math.Round(distance, fractionalDigits.Value);
            }
            return distance;
        }

        /// <summary>
        /// Creates a <seealso cref="DbGeography"/> from a given coordinate.
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public static DbGeography CreateDbGeographyPoint(double longitude, double latitude)
        {
            return DbGeography.FromText(string.Format("POINT({0} {1})", longitude, latitude), DbGeography.DefaultCoordinateSystemId);
        }

        //public static DbGeography CreateRegion(double longitude, double latitude, double milesRadius)
        //{
        //    var origin = CreateDbGeography(longitude, latitude);
        //    var distance = milesRadius * MilesToMeters;

        //    return origin.Buffer(distance);
        //}
    }
}