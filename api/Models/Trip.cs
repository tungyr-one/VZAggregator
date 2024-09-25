using System.ComponentModel.DataAnnotations;

namespace VZAggregator.Models
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }
        public IList<Order>? Orders { get; set; }
        public TripType TripType { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        //TODO: split to only date and time
        public DateTime TripDateTime { get; set; }
        public int PassengersCapacity { get; set; }
        public decimal TripPrice { get; set; }

        public int CarrierId { get; set; }
        public Carrier Carrier { get; set; }
        public int TransportId { get; set; }
        public Transport Transport { get; set; }
        public int DepartureAddressId { get; set; }
        public Address DepartureAddress { get; set; }
        public int DestinationAddressId { get; set; }
        public Address DestinationAddress { get; set; }
    }
}