using System.ComponentModel.DataAnnotations;

namespace VZAggregator.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string City { get; set; }
        public string AddressDescription { get; set; }
        public string? GeoLink { get; set; }
        public string? Coordinates { get; set; }
        public string Notes { get; set; }
    }
}