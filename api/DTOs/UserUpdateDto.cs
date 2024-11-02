using VZAggregator.Models;

namespace VZAggregator.DTOs
{
    public class UserUpdateDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string TelegramNick { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? LastTrip { get; set; }
        public DateTime Updated {get; set; }
        public string PhoneNumber { get; set; }
        public IList<Address> Addresses { get; set; }
        public int TripsCount { get; set; }

        public SubscriptionPeriod? SubscriptionType { get; set; }
        public string? Notes { get; set; }
        public string Token { get; set; }
    }
}