using System.ComponentModel.DataAnnotations;

namespace VZAggregator.Entities
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }
        public IList<Order>? Orders { get; set; }
        public TripType TripType { get; set; }
        public Address DepartureAddress { get; set; }
        public Address DestinationAddress { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime TripDateTime { get; set; }
        public int PassengersNumber { get; set; }
        public decimal TripProfit { get; set; }

        public int CarrierId { get; set; }
        public Carrier Carrier { get; set; }
        public int TransportId { get; set; }
        public Transport Transport { get; set; }
    }
}