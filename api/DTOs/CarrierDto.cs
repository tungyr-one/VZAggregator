using VZAggregator.Models;

namespace VZAggregator.DTOs
{
    public class CarrierDto
    {
        public int CarrierId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime HiringDate { get; set; }
        public decimal SalaryMonth { get; set; }
    }
}