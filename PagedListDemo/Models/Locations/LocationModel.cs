using PagedListDemo.Common.LocationUtils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;

namespace PagedListDemo.Models.Locations
{
    public class LocationModel
    {
        private DbGeography _point;
        private double _longitude;
        private double _latitude;

        public int LocationId { get; set; }
        
        public DbGeography Point
        {
            get
            {
                return _point;
            }
            set
            {
                _point = value;
                _longitude = _point.Longitude.GetValueOrDefault();
                _latitude = _point.Latitude.GetValueOrDefault();
            }
        }

        [NotMapped]
        public double Longitude
        {
            get
            {
                return _longitude;
            }
        }

        [NotMapped]
        public double Latitude
        {
            get
            {
                return _latitude;
            }
        }

        [NotMapped]
        public double? Distance { get; set; }

        [NotMapped]
        public double Miles
        {
            get
            {
                return Distance.ToMiles();
            }
        }

        [NotMapped]
        public string Address
        {
            get
            {
                var addressLines = new List<string>
                {
                    AddressLine1,
                    AddressLine2,
                    AddressLine3,
                    AddressLine4,
                    AddressLine5
                };
                var addressLine = string.Join(", ", addressLines.Where(a => !string.IsNullOrEmpty(a)));

                return string.IsNullOrEmpty(PostalCode) ? addressLine : string.Format("{0} {1}", addressLine, PostalCode);
            }
        }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string AddressLine4 { get; set; }

        public string AddressLine5 { get; set; }

        public string PostalCode { get; set; }
    }
}