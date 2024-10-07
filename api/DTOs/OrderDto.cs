
using api.Models;
using VZAggregator.Models;

namespace VZAggregator.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public int PassengersNumber { get; set; }
        public int? ChildrenNumber { get; set; }
        public decimal OrderPrice { get; set; }
        public bool IsPaid { get; set; }
        public PaymentType PaymentType { get; set; }
        public string? UserNotes { get; set; }
    }
}