using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VZAggregator.Models;

namespace api.Models
{
    public class AppUser:IdentityUser<int>
    {
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public string? Name { get; set; }
        public string TelegramNick { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? LastTrip { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public IList<Address> Addresses { get; set; }
        public int TripsCount { get; set; }

        public decimal? Discount { get; set; }
        public SubscriptionPeriod? SubscriptionType { get; set; }
        public bool? IsSubscriptionActive { get; set; }
        public int? SubscriptionTripsCount { get; set; }
        public string? Notes { get; set; }

        public IList<Order> Orders { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}