using System.ComponentModel.DataAnnotations;

namespace VZAggregator.Entities
{
    public class Carrier
    {
        [Key]
        public int CarrierId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime HiringDate { get; set; }
        public List<Transport> Transports{ get; set; }
    }
}