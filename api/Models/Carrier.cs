using System.ComponentModel.DataAnnotations;
using api.Models;

namespace VZAggregator.Models
{
    public class Carrier
    {
        [Key]
        public int CarrierId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime HiringDate { get; set; }
        public List<Transport> Transports{ get; set; }
    }
}