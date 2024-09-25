namespace VZAggregator.DTOs
{
    public class AddressDto
    {
        public int AddressId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string? GeoLink { get; set; }
        public string? Coordinates { get; set; }
        public string Notes { get; set; }
    }
}