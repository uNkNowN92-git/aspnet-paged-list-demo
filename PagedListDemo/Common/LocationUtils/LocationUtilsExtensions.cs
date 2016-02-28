using System;
using System.Data.Entity.Spatial;

namespace PagedListDemo.Common.LocationUtils
{
    public static class LocationUtilsExtensions
    {
        public static LocationUtilsFilter ToLocationFilter(this LocationUtilsFilterOptions filter, double adjustment = 0)
        {
            filter.Miles += adjustment; // adjustment (OPTIONAL)

            return new LocationUtilsFilter
            {
                Origin = LocationUtilsHelpers.CreateDbGeographyPoint(filter.Longitude, filter.Latitude),
                Radius = filter.Miles.ToMeters()
            };
        }

        /// <summary>
        /// Returns the value in miles from a given value in meters.
        /// </summary>
        /// <param name="meters"></param>
        /// <param name="fractionalDigits"></param>
        /// <returns></returns>
        public static double ToMiles(this double? meters, int? fractionalDigits = null)
        {
            var result = meters.GetValueOrDefault() / LocationUtilsConstants.MilesToMeters;

            if (fractionalDigits.HasValue && fractionalDigits >= 0)
            {
                // TODO: search for round up
                return Math.Round(result, fractionalDigits.Value);
            }
            return result;
        }

        /// <summary>
        /// Returns the value in meters from a given value in miles.
        /// </summary>
        /// <param name="miles"></param>
        /// <returns></returns>
        public static double ToMeters(this double miles)
        {
            return miles * LocationUtilsConstants.MilesToMeters;
        }
    }
}