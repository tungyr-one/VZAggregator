using api.DTOs;
using api.Models;
using VZAggregator.Models;

namespace VZAggregator.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string TelegramNick { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? LastTrip { get; set; }
        public string PhoneNumber { get; set; }
        public IList<Address> Addresses { get; set; }
        public int TripsCount { get; set; }

        public decimal? Discount { get; set; }
        public SubscriptionPeriod? SubscriptionType { get; set; }
        public bool? IsSubscriptionActive { get; set; }
        public int? SubscriptionTripsCount { get; set; }
        public string? Notes { get; set; }
        public string Token { get; set; }

        public IList<Order> Orders { get; set; }
        public ICollection<string> UserRoles {get; set; }
    }
}